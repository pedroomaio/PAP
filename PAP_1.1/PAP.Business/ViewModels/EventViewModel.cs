using PAP.DataBase;
using System;
using System.Collections.Generic;

namespace PAP.Business.ViewModels
{
    public class EventViewModel
    {
        public Guid UserId { get; set; }
        public int EventId { get; set; }
        public string  EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string  TypeOfEvent { get; set; }
        public string Location { get; set; }
        public string PhotoUrl { get; set; }
        public string  Description { get; set; }
        public DateTime  DateCreated { get; set; }
        public ICollection<AccountOnEvent> AccountOnEvent { get; set; }
        public Boolean  IsUserCreated { get; set; }
        public Boolean  IsUserJoined { get; set; }
    }
}
