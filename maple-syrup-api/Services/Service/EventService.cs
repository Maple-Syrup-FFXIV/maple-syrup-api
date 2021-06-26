using maple_syrup_api.Models;
using maple_syrup_api.Repositories.IRepository;
using maple_syrup_api.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Services.Service
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository pEventRepository)
        {
            _eventRepository = pEventRepository;
        }

        public List<Event> GetAllFromStartDate(DateTime pFilterStartDate)
        {
            return _eventRepository.GetAllFromStartDate(pFilterStartDate);
        }
    }
}
