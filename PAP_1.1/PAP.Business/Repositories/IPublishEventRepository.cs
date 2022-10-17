using PAP.Business.ViewModels.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAP.Business.Repositories
{
    public interface IPublishEventRepository
    {
        void AddEventPublish(EventPostViewModel FeedPost, Guid UserId,int EventId);
        void AddFeedBack(EventFeedIndexFeedBackViewModel FeedBack, Guid UserId);

        IEnumerable<EventFeedIndexViewModel> GetEventPublishes(int EventID);
        IEnumerable<EventFeedIndexFeedBackViewModel> GetAccountPublishFeedBack(int AccountPublishId,int EventId);
    }
}
