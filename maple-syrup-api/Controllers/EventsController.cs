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
using maple_syrup_api.Repositories.IRepository;
using maple_syrup_api.Exceptions;
using maple_syrup_api.Dto.EventManagement;

namespace maple_syrup_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IRequirementService _requirementService;
        private readonly IPlayerService _playerService;
        private readonly IUserService _userService;
        public EventsController(IEventService pEventService,IRequirementService pRequirementService, IPlayerService pPLayerService, IUserService pUserService)
        {
            _eventService = pEventService;
            _requirementService = pRequirementService;
            _playerService = pPLayerService;
            _userService = pUserService;
        }

        // POST: api/Events
        
        [HttpGet]
        public ActionResult<GetEventsFromStartDateOut> GetEventsFromStartDate(GetEventsFromStartDateIn pInput)
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
        public ActionResult<GetEventsFromStartDateOut> CreateEvent(CreateEventIn pInput)
        {
            //For now Id will be requested by the caller, but could be generated

            Event nEvent = new Event() 
            {
                StartDate = pInput.StartDate,
                EndDate = pInput.EndDate,
                EventType = (EventType) pInput.EventType,
                EventStatus = (EventStatus) 0,
                FightName = pInput.FightName,
                OwnerId = pInput.OwnerId,
                //Description = pInput.Description
            };

            EventRequirement nRequirement = new EventRequirement()
            {
                Event = nEvent,
                PreciseJob = pInput.PreciseJob,
                OnePerJob = pInput.OnePerJob,
                DPSRequiredByType = pInput.DPSRequiredByType,
                AllowBlueMage = pInput.AllowBlueMage,
                PlayerLimit = pInput.PlayerLimit,
                PlayerCount = pInput.PlayerCount,
                MinILevel = pInput.MinILevel,
                MinLevel = pInput.MinLevel,
                DPSTypeRequirement = pInput.DPSTypeRequirement,
                ClassRequirement = pInput.ClassRequirement,
                PerJobRequirement = pInput.PerJobRequirement,
                Players = new List<Player>(),
                OriginalClassRequirement = pInput.ClassRequirement,
                OriginalPerJobRequirement = pInput.PerJobRequirement,
                OriginalDPSTypeRequirement = pInput.DPSTypeRequirement
            };
            nEvent.Requirement = nRequirement;
            _eventService.CreateEvent(nEvent, nRequirement);

            return Ok(true);

        }
        //Added part
        
        [HttpPost]
        public ActionResult<bool> AddPlayerEvent(RequirementAddPlayerIn pAddPlayer)
        {



            Player newPlayer = new Player()
            {
                PlayerName = pAddPlayer.PlayerName,
                Class = pAddPlayer.Class,
                Job = pAddPlayer.Job,
                DPSType = pAddPlayer.DPSType
            };

            try
            {
                _eventService.AddPlayer(newPlayer, pAddPlayer.EventId, pAddPlayer.UserId);
            }catch(Exception e)
            {
                return Ok(e.Message);
            }

            return Ok(true);

        }
        
        [HttpPost]
        public ActionResult<bool> RemovePlayerEvent(RequirementRemovePlayerIn pRemovePlayer)
        {

            Player Player = _playerService.getPlayer(pRemovePlayer.PlayerId);
            if (Player == null) throw new MapleException("PlayerNotFound");
            try
            {
                _eventService.RemovePlayer(Player, pRemovePlayer.EventId);
            }catch(Exception e)
            {
                return Ok(e.Message);
            }
            //_userService.RemovePlayer(Player.Id, pRemovePlayer.UserId);
            //_playerService.RemovePlayer(Player);
            return Ok(true);

        }

        [HttpPost]
        public ActionResult<bool> KickPlayerEvent(RequirementRemovePlayerIn pRemovePlayer)
        {
            //Similar to removePLayeRevent, but can be used by Owner of an event to kick a player
            if (pRemovePlayer.UserId != _eventService.GetIdOwner(pRemovePlayer.EventId)) throw new MapleException("You do not have permission to change this");

            return RemovePlayerEvent(pRemovePlayer);

        }

        [HttpPost]
        public ActionResult<bool> UpdateEvent(UpdateEventIn pInput)
        {

            if (pInput.UserId != _eventService.GetIdOwner(pInput.EventId)) throw new MapleException("You do not have permission to change this");
            Event rEvent;
            try {
                rEvent = _eventService.UpdateEvent(pInput);
            }
            catch(Exception e) { return Ok(e.Message); }
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

        [HttpGet]
        public ActionResult<GetRequirementOut> GetRequirement(GetRequirementIn pInput)
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
        public ActionResult<bool> UpdateRequirement(UpdateRequirementIn pInput)
        {

            if (pInput.UserId != _eventService.GetIdOwner(pInput.EventId)) throw new MapleException("You do not have permission to change this");

            EventRequirement newRequirement = new EventRequirement()
            {
                Id = 0,//Will have to set this up in Service
                Event = null,//Willhave to set this up in Service
                EventId = pInput.EventId,
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
            try
            {
                int result = _requirementService.UpdateRequirement(newRequirement, pInput.EventId);
            }
            catch(Exception e)
            {
                return Ok(false);
            }
            //Will have to do something depending on result
            return Ok(true);
        }

        [HttpPost]

        public ActionResult<bool> DeleteEvent(DeleteEventIn pInput)
        {
            if (pInput.UserId != _eventService.GetIdOwner(pInput.EventId)) throw new MapleException("You do not have permission to change this");

            try
            {
                _eventService.DeleteEvent(pInput.EventId);
            }catch(Exception e)
            {
                return Ok(e.Message);
            }
            return Ok(true);
        }

        [HttpGet]

        public ActionResult<List<DisplayEventOut>> GetBrowseEvent(BrowseEventIn pInput)
        {
            return Ok(null);
        }

        [HttpGet]
        public ActionResult<DisplayEventOut> DisplayEvent(DisplayEventIn pInput)
        {
            //This function will be used to display an event on the website. It will manage the event's data and return it
            //in a simple and easy way for the website to use it
            DisplayEventOut result;
            try
            {
                result = _eventService.DisplayEvent(pInput.EventId);
            }catch(Exception e)
            {
                return Ok(e.Message);
            }
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
