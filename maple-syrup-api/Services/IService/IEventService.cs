using maple_syrup_api.Models;
using maple_syrup_api.Dto;
using System;
using System.Collections.Generic;


namespace maple_syrup_api.Services.IService
{
    public interface IEventService
    {
        List<Event> GetAllFromStartDate(DateTime pFilterStartDate);

        void AddPlayer(Player newPlayer,int EventId);

        void RemovePlayer(String PlayerName, int EventId);

        Event UpdateEvent(UpdateEventIn NewEvent);

        void CreateEvent(Event nEvent, EventRequirement pRequirement);
    }
}
