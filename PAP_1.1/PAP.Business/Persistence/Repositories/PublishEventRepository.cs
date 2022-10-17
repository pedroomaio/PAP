using PAP.Business.DbContext;
using PAP.Business.Repositories;
using PAP.Business.ViewModels.Event;
using PAP.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PAP.Business.Persistence.Repositories
{
    public class PublishEventRepository : IPublishEventRepository
    {
        private readonly ApplicationDatabaseContext _context;

        public PublishEventRepository(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        public void AddEventPublish(EventPostViewModel FeedPost, Guid UserId,int EventId)
        {
            var publishEvent = new PublishEvent()
            {
                EventId  = EventId,  
                AccountId = UserId,
                PublishDate = DateTime.Now,
            };

            _context.PublishEvent.Add(publishEvent);

            var contentPublishEvent = new ContentPublishEvent()
            {
                PublishEventId = publishEvent.PublishEventId,
                TextContent = FeedPost.TextOnPublish
            };

            _context.ContentPublishEvent.Add(contentPublishEvent);

            var photoContentPublishEvent = new PhotoContentPublishEvent()
            {
                ContentPublishEventId = contentPublishEvent.ContentPublishEventId,
                PhotoURl = FeedPost.Path
            };
            _context.PhotoContentPublishEvent.Add(photoContentPublishEvent);
        }

        public IEnumerable<EventFeedIndexViewModel> GetEventPublishes(int EventId)
        {
            var EventPublish = from pe in _context.PublishEvent
                               join ac in _context.Users
                               on pe.AccountId equals ac.Id
                               join ce in _context.ContentPublishEvent
                               on pe.PublishEventId equals ce.PublishEventId
                               join pce in _context.PhotoContentPublishEvent
                               on ce.ContentPublishEventId equals pce.ContentPublishEventId
                               where pe.EventId == EventId 
                               select new EventFeedIndexViewModel
                               {
                                   ContentPublishId = ce.ContentPublishEventId,
                                   AccountPublishId = pe.PublishEventId,
                                   TextOnPublish = ce.TextContent,
                                   UserNick = ac.NickName,
                                   UserPublishPhoto = ac.PhotoUrl,
                                   PhotoPath = pce.PhotoURl,
                                   EventFeedIndexFeedBacks = GetAccountPublishFeedBack(pe.PublishEventId,pe.EventId)
                               };

            return EventPublish;
        }

        public IEnumerable<EventFeedIndexFeedBackViewModel> GetAccountPublishFeedBack(int PublishEventId,int EventId)
        {
            var FeedEventPublishFeedBack = from FBCA in _context.FeedBackContentEvent 
                                           join AC in _context.Users 
                                           on FBCA.AccountId equals AC.Id
                                           join EVP in _context.PublishEvent
                                           on FBCA.EventPublishId equals EVP.PublishEventId                                         
                                           where FBCA.EventPublishId  == PublishEventId && EVP.EventId == EventId 
                                           select new EventFeedIndexFeedBackViewModel()
                                           {
                                               EventFeedBackText = FBCA.Description,
                                               UserNick = AC.NickName,
                                               UserPhotoFeedBack = AC.PhotoUrl
                                           };

            return FeedEventPublishFeedBack;

        }


        public void AddFeedBack(EventFeedIndexFeedBackViewModel FeedBack, Guid UserId)
        {
            var accountPublishFeedBack = new FeedBackContentEvent()
            {    
                AccountId = UserId,
                 EventPublishId  = FeedBack.EventPublishId,
                Description = FeedBack.EventFeedBackText
            };

            _context.FeedBackContentEvent.AddAsync(accountPublishFeedBack);
        }
    }
}
