using PAP.Business.DbContext;
using PAP.Business.Repositories;
using PAP.Business.ViewModels;
using System;
using System.Collections.Generic;
using PAP.DataBase;
using System.Linq;
using System.IO;

namespace PAP.Business.Persistence.Repositories
{
    public class PublishAccountRepository : IPublishAccountRepository
    {
        private readonly ApplicationDatabaseContext _context;

        public PublishAccountRepository(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        public void AddAccountPublish(FeedPostViewModel feedPost, Guid userId)
        {
            var AccountPublish = new AccountPublish()
            {
                AccountId = userId,
                DatePublish = DateTime.Now,
            };

            _context.AccountPublish.Add(AccountPublish);

            var contentPublishAccount = new ContentPublishAccount()
            {
                AccountPublishId = AccountPublish.AccountPublishId,
                TextContent = feedPost.TextOnPublish
            };

            _context.ContentPublishAccount.Add(contentPublishAccount);

            var photoContentPublishAccount = new PhotoContentPublishAccount()
            {
                ContentPublishAccountId = contentPublishAccount.ContentPublishAccountId,
                PhotoURl = feedPost.Path
            };
            _context.PhotoContentPublishAccount.Add(photoContentPublishAccount);
        }

        public IEnumerable<FeedIndexViewModel> GetAccountPublishes()
        {
            var accountpublish = (from ap in _context.AccountPublish
                                  join ac in _context.Users
                                  on ap.AccountId equals ac.Id
                                  join cpa in _context.ContentPublishAccount
                                  on ap.AccountPublishId equals cpa.AccountPublishId
                                  join pcpa in _context.PhotoContentPublishAccount
                                  on cpa.ContentPublishAccountId equals pcpa.ContentPublishAccountId
                                  select new FeedIndexViewModel()
                                  {
                                      ContentPublishId = cpa.ContentPublishAccountId,
                                      AccountPublishId = ap.AccountPublishId,
                                      TextOnPublish = cpa.TextContent,
                                      UserNick = ac.NickName,
                                      UserPublishPhoto = ac.PhotoUrl,
                                      PhotoPath = pcpa.PhotoURl,
                                      feedIndexFeedBacks = GetAccountPublishFeedBack(ap.AccountPublishId)
                                  }).ToList();

            return accountpublish;
        }

        public IEnumerable<FeedIndexFeedBackViewModel> GetAccountPublishFeedBack(int AccountPublishId)
        {
            var FeedAccountPublishFeedBack = from FBCA in _context.FeedBackContentAccount 
                                             join AC in _context.Users
                                             on FBCA.AccountId equals AC.Id
                                             where FBCA.AccountPublishId == AccountPublishId
                                             select new FeedIndexFeedBackViewModel()
                                             {
                                                 FeedBackText = FBCA.Description,
                                                 UserNick = AC.NickName,
                                                 UserPhotoFeedBack = AC.PhotoUrl
                                             };

            return FeedAccountPublishFeedBack;

        }


        public void AddFeedBack(FeedIndexFeedBackViewModel FeedBack, Guid UserId)
        {
            var accountPublishFeedBack = new FeedBackContentAccount()
            {
                AccountId = UserId,
                AccountPublishId = FeedBack.AccountPublishId,
                Description = FeedBack.FeedBackText
            };
            _context.FeedBackContentAccount.AddAsync(accountPublishFeedBack);
        }
    }
}

