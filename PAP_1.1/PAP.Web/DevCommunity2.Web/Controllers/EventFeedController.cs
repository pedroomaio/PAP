using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAP.Business.Managers;
using PAP.Business.Persistence.Repositories;
using PAP.Business.Repositories;
using PAP.Business.ViewModels;
using PAP.Business.ViewModels.Event;

namespace DevCommunity2.Web.Controllers
{
    public class EventFeedController : Controller
    {
        private readonly PublishEventRepository _publishEventRepository;
        private readonly BaseManager _BaseManager;
        private readonly HostingEnvironment _hostingEnvironment;
       


        public EventFeedController(IPublishEventRepository publishEventRepository, BaseManager baseManager, IHostingEnvironment hostingEnvironment)
        {
            _publishEventRepository = (PublishEventRepository)publishEventRepository;
            _BaseManager = baseManager;
            _hostingEnvironment = (HostingEnvironment)hostingEnvironment;
        }

        public ActionResult Index(int EventId)
        {

            if (EventId == 0)
            {
                EventId = (int)TempData["EventId"]; 
            }
            if (EventId != 0)
             {
                var result = _publishEventRepository.GetEventPublishes(EventId).ToList();
                TempData["EventId"] = EventId;
               
                return View(result);
            }
            return BadRequest();
        }

        // GET: Feed/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Feed/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EventPostViewModel FeedPost, IFormFile File)
        {
            try
            {
                Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

                if (File != null)
                {

                    var uploadFolder = Path.Combine(
                        _hostingEnvironment.WebRootPath, "Images", "PublishEvent");
                    var uniqueFileName = Guid.NewGuid() + File.FileName;

                    var path = Path.Combine(uploadFolder, uniqueFileName);


                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await File.CopyToAsync(stream);
                    }
                    string pic = System.IO.Path.GetFileName(File.FileName);

                    FeedPost.Path = uniqueFileName;
                }
                FeedPost.EventId = int.Parse(TempData["EventId"].ToString());

                TempData.Keep("EventId");
                _publishEventRepository.AddEventPublish(FeedPost, userId, FeedPost.EventId);
                _BaseManager.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "erro" });
                throw;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFeedBack(EventFeedIndexViewModel feedIndexViewModel)
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

            if (userId == null)
            {
                return Json(new { success = false, message = "erro" });
            }

            var fifvm = new EventFeedIndexFeedBackViewModel()
            {
                EventPublishId = feedIndexViewModel.AccountPublishId,
                EventFeedBackText = feedIndexViewModel.FeedBackText
            };

            try
            {

                _publishEventRepository.AddFeedBack(fifvm, userId);
                _BaseManager.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }
    }
}