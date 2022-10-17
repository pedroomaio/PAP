using PAP.DataBase.Auth;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PAP.DataBase
{
    public class FeedBackContentEvent
    {
        [Key]
        public int IdFeedBackContent { get; set; }

        public int EventPublishId { get; set; }

        public Guid AccountId { get; set; }

        public int Stars { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [ForeignKey(nameof(AccountId))]
        public virtual User Account { get; set; }

        [ForeignKey(nameof(EventPublishId))]
        public virtual PublishEvent PublishEvent  { get; set; }
    }
}
