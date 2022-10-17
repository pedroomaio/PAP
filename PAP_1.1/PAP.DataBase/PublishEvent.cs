using PAP.DataBase.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PAP.DataBase
{
    public class PublishEvent
    {
        [Key]
        public int PublishEventId { get; set; }

        public int EventId { get; set; }

        public Guid  AccountId { get; set; }

        public DateTime PublishDate { get; set; }

        [ForeignKey(nameof(AccountId))]
        public virtual User Account { get; set; }

        public ContentPublishEvent ContentPublishEvent { get; set; }

        [InverseProperty(nameof(FeedBackContentEvent.PublishEvent))]
        public virtual ICollection<FeedBackContentEvent> FeedBackContentEvents { get; set; }

        [ForeignKey(nameof(EventId))]
        public virtual Event Event { get; set; }
    }
}
