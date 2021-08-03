using maple_syrup_api.Models;
using System;
using System.Collections.Generic;


namespace maple_syrup_api.Services.IService
{
    public interface IEventService
    {
        List<Event> GetAllFromStartDate(DateTime pFilterStartDate);

        void AddPlayer(Player newPlayer,int EventId);

        void RemovePlayer(String PlayerName, int EventId);
    }
}
