using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using PAP.Business.DbContext;
using PAP.Business.Repositories;
using PAP.Business.ViewModels;
using PAP.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PAP.Business.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {

        private readonly ApplicationDatabaseContext _context;

        public EventRepository(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        public EventViewModel Get(int id)
        {
            var @event = _context.Event.Select(e => new EventViewModel()
            {
                EventId = e.EventId,
                EventName = e.NameEvent,
                EventDate = e.DateEvent,
                TypeOfEvent = e.TypeOfEvent,
                Location = e.Location
            }).FirstOrDefault(e => e.EventId == id);

            return @event;
        }

        public IEnumerable<EventViewModel> GetAll(Guid userId)
        {

            var @event = (from EV in _context.Event
                          join AE in _context.AccountOnEvent
                          on EV.EventId equals AE.EventId
                          select new EventViewModel()
                          {
                              DateCreated = EV.DateCreated,
                              Description = EV.Description,
                              EventId = EV.EventId,
                              EventName = EV.NameEvent,
                              EventDate = EV.DateEvent,
                              TypeOfEvent = EV.TypeOfEvent,
                              Location = EV.Location,
                              IsUserCreated = EV.CreatedByUserID == userId,
                              IsUserJoined = AE.AccountId == userId
                          }).ToList();
          
            return @event;
        }



        public EventViewModel GetEventByUser(Guid userId)
        {
            var @event = _context.Event.Select(e => new EventViewModel()
            {
                UserId = userId,
                EventId = e.EventId,
                EventName = e.NameEvent,
                EventDate = e.DateEvent,
                TypeOfEvent = e.TypeOfEvent,
                Location = e.Location
            }).FirstOrDefault(e => e.UserId == userId);
            return @event;
        }


        public bool IsUserEventCreated(Guid UserId, Event Event)
        {
            if (Event != null)
            {
                if (Event.CreatedByUserID == UserId)
                {
                    return true;
                }
            }          
            return false;
        }

        public EventViewModel GetEventsNameByUser(Guid userId)
        {
            var @event = _context.Event.Select(e => new EventViewModel()
            {        
                EventName = e.NameEvent
                
            }).FirstOrDefault(e => e.UserId == userId);

            return @event;

        }


        public Boolean IsUserOnEvent(Guid UserID , Event @event)
        {
                  
            if (@event != null)
            {
                if (@event.AccountOnEvents.Count > 0)
                {
                    foreach (var item in @event.AccountOnEvents)
                    {
                        if (item.AccountId == UserID)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
              
            }

            return false;
        }

        public void JoinOnEvent(int EventId, Guid UserId)
        {
            var accountOnEvent = new AccountOnEvent()
            {
                AccountId = UserId,
                EventId = EventId
            };

            _context.AccountOnEvent.Add(accountOnEvent);
        }

        public void UnJoinEvent(int EventId, Guid UserId)
        {
            var accountOnEvent = (from AE in _context.AccountOnEvent
                                  where AE.EventId == EventId && AE.AccountId == UserId
                                  select AE
                                  ).First();                                

            _context.AccountOnEvent.Remove(accountOnEvent);
        }

        public void Add(EventViewModel entity, Guid userId)
        {
            if (entity.PhotoUrl == null)
            {
                entity.PhotoUrl = "~/Images/EventPhotos/DefaulEventPhoto.png";
            }

            var @event = new Event()
            {
                DateCreated = DateTime.Now.Date,
                DateEvent = entity.EventDate,
                Description = entity.Description,
                Location = entity.Location,
                NameEvent = entity.EventName,
                PhotoUrl = entity.PhotoUrl,
                TypeOfEvent = entity.TypeOfEvent,
                CreatedByUserID = userId
                
            };

            _context.Event.Add(@event);

            if (userId != null)
            {
              
                var accountonEvents = new AccountOnEvent()
                {
                    AccountId = userId,
                    EventId = @event.EventId
                };

                _context.AccountOnEvent.Add(accountonEvents);
            }
        }

        public void Remove(EventViewModel entity)
        {
            var @event = _context.Event.Find(entity.EventId);
            @event.IsEnabled = false;
        }

        public void EditEvent(EventViewModel entity)
        {
            var @event = _context.Event.Find(entity.EventId);

            @event.DateEvent = entity.EventDate;
            @event.Description = entity.Description;
            @event.Location = entity.Location;
            @event.NameEvent = entity.EventName;                         
        }
    }

}
