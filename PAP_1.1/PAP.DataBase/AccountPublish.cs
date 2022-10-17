using PAP.DataBase.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PAP.DataBase
{

    public  class AccountPublish
    {

        [Key]
        public int AccountPublishId { get; set; }

        public Guid AccountId { get; set; }

        public DateTime DatePublish { get; set; }

        [ForeignKey(nameof(AccountId))]
        public virtual User Account { get; set; }
     
        [InverseProperty(nameof(FeedBackContentAccount.AccountPublish))]
        public virtual ICollection<FeedBackContentAccount> FeedBackContentAccounts { get; set; }

        public ContentPublishAccount ContentPublishAccounts { get; set; }

    }
}
