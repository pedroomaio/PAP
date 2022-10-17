using PAP.Business.ViewModels;
using PAP.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAP.Business.Repositories
{
    public interface IEventRepository
    {
        //GETS
        EventViewModel Get(int id);
        IEnumerable<EventViewModel> GetAll(Guid userId);
        EventViewModel GetEventsNameByUser(Guid UserId);
        Boolean IsUserEventCreated(Guid UserId, Event @event);
        Boolean IsUserOnEvent(Guid UserID, Event @event);

        //SETS
        void Add(EventViewModel @event,Guid UserId);
        void Remove(EventViewModel @event);
        void EditEvent(EventViewModel @event);        
        void JoinOnEvent(int EventId, Guid UserId);
        void UnJoinEvent(int EventId, Guid UserId);
    }
}
