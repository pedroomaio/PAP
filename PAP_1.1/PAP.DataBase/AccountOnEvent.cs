using PAP.DataBase.Auth;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAP.DataBase
{
    public  class AccountOnEvent
    {
        [Key]
        public int AccountOnEventId { get; set; }

        public Guid AccountId { get; set; }

        public int EventId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public virtual User Account { get; set; }
        [ForeignKey(nameof(EventId))]
        public virtual Event Event { get; set; }
    }
}
