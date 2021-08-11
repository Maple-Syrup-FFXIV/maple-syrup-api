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
        private readonly IRequirementService _requirementService;
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
        public async Task<ActionResult<GetEventsFromStartDateOut>> CreateEvent(CreateEventIn pInput)
        {
            //For now Id will be requested by the caller, but could be generated

            Event nEvent = new Event() 
            { 
                Id = pInput.Id,
                StartDate = pInput.StartDate,
                EndDate = pInput.EndDate,
                EventType = (EventType) pInput.EventType,
                EventStatus = (EventStatus) 0,
                FightName = pInput.FightName
            };

            EventRequirement nRequirement = new EventRequirement()
            {
                Id = pInput.RId,
                Event = nEvent,
                EventId = nEvent.Id,
                PreciseJob = pInput.PreciseJob,
                OnePerJob = pInput.OnePerJob,
                DPSRequiredByType = pInput.DPSRequiredByType,
                AllowBlueMage = pInput.AllowBlueMage,
                DPSTypeRequirement = pInput.DPSTypeRequirement,
                ClassRequirement = pInput.ClassRequirement,
                PerJobRequirement = pInput.PerJobRequirement,
                PlayerLimit = pInput.PlayerLimit,
                PlayerCount = pInput.PlayerCount,
                MinILevel = pInput.MinILevel,
                MinLevel = pInput.MinLevel,
                Players = new List<Player>(),
            };
            nEvent.Requirement = nRequirement;
            nEvent.RequirementId = nRequirement.Id;

            _eventService.CreateEvent(nEvent, nRequirement);

            return Ok(true);

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

            return Ok(true);

        }
        [HttpPost]
        public async Task<ActionResult> RemovePlayerEvent(RequirementRemovePlayerIn pRemovePlayer)
        {
            _eventService.RemovePlayer(pRemovePlayer.PlayerName, pRemovePlayer.EventId);

            return Ok(true);

        }
        [HttpPost]
        public async Task<ActionResult> UpdateEvent(UpdateEventIn pInput)
        {
            Event rEvent = _eventService.UpdateEvent(pInput);
            UpdateEventOut result = new UpdateEventOut
            {
                StartDate = rEvent.StartDate,
                EndDate = rEvent.EndDate,
                EventType = rEvent.EventType,
                EventStatus = rEvent.EventStatus,
                FightName = rEvent.FightName,
                Id = rEvent.Id
            };

            return Ok(result);

        }
        [HttpPost]
        public async Task<ActionResult> GetRequirement(GetRequirementIn pInput)
        {
            EventRequirement rReq = _requirementService.GetRequirement(pInput.EventId);

            GetRequirementOut result = new GetRequirementOut()
            {
                PreciseJob = rReq.PreciseJob,
                OnePerJob= rReq.OnePerJob,
                DPSRequiredByType = rReq.DPSRequiredByType,
                AllowBlueMage = rReq.AllowBlueMage,
                DPSTypeRequirement = rReq.DPSTypeRequirement,
                ClassRequirement = rReq.ClassRequirement,
                PerJobRequirement = rReq.PerJobRequirement,
                Players = rReq.Players,
                PlayerLimit = rReq.PlayerLimit,
                PlayerCount = rReq.PlayerCount,
                MinILevel = rReq.MinILevel,
                MinLevel = rReq.MinLevel
            };
            
            return Ok(result);

        }

        [HttpPost]
        public async Task<ActionResult> UpdateRequirement(UpdateRequirementIn pInput, int EventId)
        {



            EventRequirement newRequirement = new EventRequirement()
            {
                Id = 0,//Will have to set this up in Service
                Event = null,//Willhave to set this up in Service
                EventId = EventId,
                PreciseJob = pInput.PreciseJob,
                OnePerJob = pInput.OnePerJob,
                DPSRequiredByType = pInput.DPSRequiredByType,
                AllowBlueMage = pInput.AllowBlueMage,
                DPSTypeRequirement = pInput.DPSTypeRequirement,
                ClassRequirement = pInput.ClassRequirement,
                PerJobRequirement = pInput.PerJobRequirement,
                Players = new List<Player>(),
                PlayerLimit = pInput.PlayerLimit,
                PlayerCount = 0,
                MinILevel = pInput.MinILevel,
                MinLevel = pInput.MinLevel
            };

            int result = _requirementService.UpdateRequirement(newRequirement, EventId);
            //Will have to do something depending on result
            return Ok(result);

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
