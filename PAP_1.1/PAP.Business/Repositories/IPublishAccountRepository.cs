using Microsoft.AspNetCore.Http;
using PAP.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAP.Business.Repositories
{
    public interface IPublishAccountRepository
    {
        void AddAccountPublish(FeedPostViewModel accountPublish,Guid userId);

        IEnumerable<FeedIndexViewModel> GetAccountPublishes();

        void AddFeedBack(FeedIndexFeedBackViewModel FeedBack, Guid UserId);

        IEnumerable<FeedIndexFeedBackViewModel> GetAccountPublishFeedBack(int AccountPublishId);
    }
}
