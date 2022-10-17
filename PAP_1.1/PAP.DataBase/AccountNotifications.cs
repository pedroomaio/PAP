using PAP.DataBase.Auth;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PAP.DataBase
{
    public  class AccountNotification
    {
        [Key]
        public int AccountNotificationsId { get; set; }
        [Required]
        public Guid SenderNotificationAccountId { get; set; }

        public int EventId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Text { get; set; }

        [Column(TypeName = "date")]
        public DateTime NotificationsDate { get; set; }

        public string RedirectUrl { get; set; }

        public int AccountId { get; set; }
        public bool Seen { get; set; }
        [Required]
        public Guid ReceiverNotificationAccountId { get; set; }
     
        [ForeignKey(nameof(EventId))]
        public virtual Event Event { get; set; }
        [ForeignKey(nameof(SenderNotificationAccountId))]
        public virtual User SenderNotificationAccount { get; set; }
        [ForeignKey(nameof(ReceiverNotificationAccountId))]
        public virtual User ReceiverNotificationAccount { get; set; }
    }
}
