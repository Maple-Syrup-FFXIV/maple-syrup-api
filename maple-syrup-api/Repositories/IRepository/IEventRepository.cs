using maple_syrup_api.Models;
using System;
using System.Collections.Generic;

namespace maple_syrup_api.Repositories.IRepository
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        List<Event> GetAllFromStartDate(DateTime pFilterStartDate);

        Event GetEventWithPlayers(int pEventId);

        List<Event> GetAllAtGivenDate(DateTime pDate);

    }
}
