using maple_syrup_api.Context;
using maple_syrup_api.Models;
using maple_syrup_api.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace maple_syrup_api.Repositories.Repository
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        private readonly MapleSyrupContext _context;
        public EventRepository(MapleSyrupContext pContext) : base(pContext)
        {
            _context = pContext;
        }

        public List<Event> GetAllFromStartDate(DateTime pFilterStartDate)
        {
            var query = _context.Set<Event>().Where(x => x.StartDate > pFilterStartDate);

            var result = query.ToList();
            return result;
        }
        
        public Event GetEventWithPlayers(int pEventId){
            return _context.Set<Event>().Include(s => s.Requirement.Players).FirstOrDefault(x=> x.Id == pEventId);;
        }

    }
}
