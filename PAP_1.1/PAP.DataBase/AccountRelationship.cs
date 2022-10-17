using PAP.DataBase.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PAP.DataBase
{

    public  class AccountRelationship
    {
        [Key]
        public int AccountRelationshipId { get; set; }
        [Required]
        public Guid SenderAccountId { get; set; }
        [Required]
        public Guid ReceiverAccountId { get; set; }
        public bool Isfriend { get; set; }
        [ForeignKey(nameof(SenderAccountId))]
        public virtual User SenderAccount { get; set; }
        [ForeignKey(nameof(ReceiverAccountId))]
        public virtual User ReceiverAccount { get; set; }
    }
}
