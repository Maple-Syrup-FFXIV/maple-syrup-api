using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using maple_syrup_api.Context;
using maple_syrup_api.Models;
using maple_syrup_api.Services.IService;
using maple_syrup_api.Dto;

namespace maple_syrup_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController( IEventService pEventService)
        {
            _eventService = pEventService;
        }

        // POST: api/Events
        [HttpPost]
        public async Task<ActionResult<GetEventsFromStartDateOut>> GetEventsFromStartDate(GetEventsFromStartDateIn pInput)
        {
            var eventList = _eventService.GetAllFromStartDate(pInput.StartDate);

            var result = new GetEventsFromStartDateOut()
            {
                EventList = eventList.Select(s => new PartialEventGetEventsFromStartDate()
                {
                    Id = s.Id,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    EventStatus = s.EventStatus,
                    EventType = s.EventType
                }).ToList()
            };

            return result;
        }

        // POST: api/Events
        [HttpPost]
        public async Task<ActionResult<GetEventsFromStartDateOut>> CreateOrUpdateEvent(CreateOrUpdateEventIn pInput)
        {

            var eventList = _eventService.GetAllFromStartDate((System.DateTime) pInput.StartDate);

            var result = new GetEventsFromStartDateOut()
            {
                EventList = eventList.Select(s => new PartialEventGetEventsFromStartDate()
                {
                    Id = s.Id,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    EventStatus = s.EventStatus,
                    EventType = s.EventType
                }).ToList()
            };

            return result;

        }

        //Added part
        [HttpPost]
        public async Task<ActionResult> AddPlayerEvent(RequirementAddPlayerIn pAddPlayer)
        {
            Player newPlayer = new Player()
            {
                PlayerName = pAddPlayer.PlayerName,
                Class = pAddPlayer.Class,
                Job = pAddPlayer.Job,
                DPSType = pAddPlayer.DPSType
            };

            _eventService.AddPlayer(newPlayer, pAddPlayer.EventId);

            return null;

        }

        public async Task<ActionResult> RemovePlayerEvent(RequirementRemovePlayerIn pRemovePlayer)
        {
            _eventService.RemovePlayer(pRemovePlayer.PlayerName, pRemovePlayer.EventId);

            return null;

        }



        //// GET: api/Events
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        //{
        //    return await _context.Events.ToListAsync();
        //}

        //// GET: api/Events/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Event>> GetEvent(int id)
        //{
        //    var @event = await _context.Events.FindAsync(id);

        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    return @event;
        //}

        //// PUT: api/Events/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEvent(int id, Event @event)
        //{
        //    if (id != @event.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(@event).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EventExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Events
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Event>> PostEvent(Event @event)
        //{
        //    _context.Events.Add(@event);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        //}

        //// DELETE: api/Events/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEvent(int id)
        //{
        //    var @event = await _context.Events.FindAsync(id);
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Events.Remove(@event);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool EventExists(int id)
        //{
        //    return _context.Events.Any(e => e.Id == id);
        //}
    }
}
