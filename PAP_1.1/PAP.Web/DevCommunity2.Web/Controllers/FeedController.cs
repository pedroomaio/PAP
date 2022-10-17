using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAP.Business.Managers;
using PAP.Business.Persistence.Repositories;
using PAP.Business.Repositories;
using PAP.Business.ViewModels;

namespace DevCommunity2.Web.Controllers
{
    [Authorize]
    public class FeedController : Controller
    {
        private readonly PublishAccountRepository _PublishAccountRepo;
        private readonly BaseManager _BaseManager;
        private readonly HostingEnvironment _hostingEnvironment;
    

        public FeedController(IPublishAccountRepository PublishAccountRepo, BaseManager baseManager, IHostingEnvironment hostingEnvironment)
        {
            _PublishAccountRepo = (PublishAccountRepository)PublishAccountRepo;
            _BaseManager = baseManager;
            _hostingEnvironment = (HostingEnvironment)hostingEnvironment;
        }     
        // GET: Feed
        public ActionResult Index()
        {
           
            var result = _PublishAccountRepo.GetAccountPublishes().ToList();
            return View(result);
        }

        // GET: Feed/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Feed/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FeedPostViewModel FeedPost, IFormFile File)
        {
            try
            {
                Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
                if (File != null)
                {
                    var uploadFolder = Path.Combine(
                        _hostingEnvironment.WebRootPath, "Images", "AccountPublish");
                    var uniqueFileName = Guid.NewGuid() + File.FileName;

                    var path = Path.Combine(uploadFolder, uniqueFileName);


                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await File.CopyToAsync(stream);
                    }
               
                    string pic = System.IO.Path.GetFileName(File.FileName);

                    FeedPost.Path = uniqueFileName;
                }
                _PublishAccountRepo.AddAccountPublish(FeedPost, userId);
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
        public ActionResult CreateFeedBack(FeedIndexViewModel feedIndexViewModel)
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

            if (userId == null)
            {
                return Json(new { success = false, message = "erro" });
            }

            var fifvm = new FeedIndexFeedBackViewModel()
            {
                AccountPublishId = feedIndexViewModel.AccountPublishId,
                FeedBackText = feedIndexViewModel.FeedBackText
            };

            try
            {
                
                _PublishAccountRepo.AddFeedBack(fifvm, userId);
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