using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAP.DataBase.Auth
{
    public class User : IdentityUser<Guid>
    {      
        public string NickName { get; set; }
        public string  PhotoUrl { get; set; }
     
     
        [InverseProperty(nameof(AccountOnEvent.Account))]
        public virtual ICollection<AccountOnEvent> AccountOnEvents { get; set; }

        [InverseProperty(nameof(PublishEvent.Account))]
        public virtual ICollection<PublishEvent> PublishEvents { get; set; }

        [InverseProperty(nameof(AccountPublish.Account))]
        public virtual ICollection<AccountPublish> AccountPublishes  { get; set; }

       
        [InverseProperty(nameof(FeedBackContentAccount.Account))]
        public virtual  ICollection<FeedBackContentAccount> FeedBackContentAccounts { get; set; }

        [InverseProperty(nameof(FeedBackContentEvent.Account))]
        public virtual ICollection<FeedBackContentEvent> FeedBackContentEvents { get; set; }

      [InverseProperty(nameof(AccountRelationship.SenderAccount))]
        public virtual ICollection<AccountRelationship> SenderAccountRelationships { get; set; }

       [InverseProperty(nameof(AccountRelationship.ReceiverAccount))]
        public virtual ICollection<AccountRelationship> ReceiverAccountRelationships { get; set; }

      [InverseProperty(nameof(AccountNotification.SenderNotificationAccount))]
        public virtual ICollection<AccountNotification> SenderNotificationAccounts { get; set; }

      [InverseProperty(nameof(AccountNotification.ReceiverNotificationAccount))]
        public virtual ICollection<AccountNotification> ReceiverNotificationAccounts { get; set; }

    }
}
