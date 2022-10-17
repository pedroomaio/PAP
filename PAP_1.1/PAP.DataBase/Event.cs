using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PAP.DataBase
{

    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [StringLength(20)]
        public string NameEvent { get; set; }


        public DateTime DateCreated { get; set; }


        public DateTime DateEvent { get; set; }

        [StringLength(20)]
        public string TypeOfEvent { get; set; }

        public string LocationWhat3words { get; set; }

        public string Location { get; set; }

        [StringLength(500)]
        public string PhotoUrl { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public int Stars { get; set; }

        public bool  IsEnabled { get; set; }

        [Required]
        public Guid CreatedByUserID { get; set; }

        [InverseProperty(nameof(AccountNotification.Event))]
        public virtual ICollection<AccountNotification> AccountNotifications { get; set; }

        [InverseProperty(nameof(AccountOnEvent.Event))]
        public virtual ICollection<AccountOnEvent> AccountOnEvents { get; set; }
    

        [InverseProperty(nameof(PublishEvent.Event))]
        public virtual ICollection<PublishEvent> PublishEvents { get; set; }
    }
}
