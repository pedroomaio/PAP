using PAP.DataBase.Auth;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PAP.DataBase
{

    public class FeedBackContentAccount
    {
        [Key]
        public int FeedBackContentAccountId { get; set; }

        public int AccountPublishId { get; set; }

        public Guid AccountId { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int Stars { get; set; }

        [ForeignKey(nameof(AccountId))]
        public virtual User Account { get; set; }

        [ForeignKey(nameof(AccountPublishId))]
        public virtual AccountPublish AccountPublish { get; set; }
    }
}
