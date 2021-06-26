using maple_syrup_api.Models;
using System;
using System.Collections.Generic;

namespace maple_syrup_api.Repositories.IRepository
{
    public interface IEventRepository
    {
        List<Event> GetAllFromStartDate(DateTime pFilterStartDate);

    }
}
