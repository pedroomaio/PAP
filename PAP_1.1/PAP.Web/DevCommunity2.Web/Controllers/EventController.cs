using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAP.Business.Managers;
using PAP.Business.Persistence.Repositories;
using PAP.Business.Repositories;
using PAP.Business.ViewModels;

namespace DevCommunity2.Web.Controllers
{
    [Authorize]
    public class EventController : Controller
    {

        private readonly EventRepository _eventRepo;
        private readonly BaseManager _BaseManager;

        public EventController(IEventRepository eventRepo, BaseManager baseManager)
        {
            _eventRepo = (EventRepository)eventRepo;
            _BaseManager = baseManager;
        }


        
        [HttpPost]
        public ActionResult JoinOnEvent(int eventId)
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

            if (userId == null)
            {
                return Json(new { sucess = false });
            }

            _eventRepo.JoinOnEvent(eventId, userId);
            _BaseManager.SaveChanges();

             return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public ActionResult UnJoinOnEvent(int eventId)
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

            if (userId == null)
            {
                return Json(new { sucess = false });
            }

            _eventRepo.UnJoinEvent(eventId, userId);
            _BaseManager.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Event
        public ActionResult Index()
        {
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
            if (userId != null )
            {
                return View(_eventRepo.GetAll(userId));
            }
            return View();
        }    

        // GET: Event/Create
        
        public ActionResult Create()
        {
            return View();
        }
      
        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventViewModel @event)
        {
           
                //  string userid = User.
                //Guid.TryParse(userid, out Guid usertst);
                // TODO: Add insert logic here     
                Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
                _eventRepo.Add(@event, userId);
                _BaseManager.SaveChanges();
                return RedirectToAction(nameof(Index));
            
           
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {

                return View();
            }
            catch (Exception)
            {
                return View();
                throw;
            }
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventViewModel @event, int id)
        {
            // TODO: Add update logic here
            try
            {
                @event.EventId = id;
                _eventRepo.EditEvent(@event);
                _BaseManager.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
                throw;
            }
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Event/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(EventViewModel @event)
        {
            try
            {
                // TODO: Add delete logic here
                //_eventRepo.Remove(@event);
                //_eventRepo.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}