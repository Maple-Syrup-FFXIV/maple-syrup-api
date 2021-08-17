using maple_syrup_api.Models;
using maple_syrup_api.Dto;
using maple_syrup_api.Repositories.IRepository;
using maple_syrup_api.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using maple_syrup_api.Exceptions;
using maple_syrup_api.Dto.EventManagement;

namespace maple_syrup_api.Services.Service
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IRequirementRepository _requirementRepository;
        private readonly IRequirementService _requirementService;
        private readonly IUserService _userService;
        private readonly IPlayerService _playerService;

        public EventService(IEventRepository pEventRepository,IRequirementRepository pRequirementRepository,IRequirementService pRequirementService, IUserService pUserService, IPlayerService pPlayerService)
        {
            _eventRepository = pEventRepository;
            _requirementRepository = pRequirementRepository;
            _requirementService = pRequirementService;
            _userService = pUserService;
            _playerService = pPlayerService;
        }

        public List<Event> GetAllFromStartDate(DateTime pFilterStartDate)
        {
            return _eventRepository.GetAllFromStartDate(pFilterStartDate);
        }

        public void CreateOrUpdate(Event pEvent)
        {
            _eventRepository.AddOrUpdate(pEvent);
        }

        //ADDED PART

        public void AddPlayer(Player player, int EventId)
        {

            Event pEvent = _eventRepository.Get(EventId);

            if (pEvent.Requirement.PlayerCount < pEvent.Requirement.PlayerLimit)
            {
                int result = _requirementService.AddPlayer(player, pEvent.Requirement);

                if (result == 0)
                {
                    //Success
                    //We will change the EventRequirement accordingly and save it to the database

                    //The Requirements have already been changed, the only thing left is to add the Player to the list

                    pEvent.Requirement.Players.Add(player);//Add player
                
                    _requirementRepository.AddOrUpdate(pEvent.Requirement);

                    _requirementRepository.Save();
                }
                else if (result == 1)
                {
                    //throw new MappleException("ClassNotNeeded");
                }
                else if (result == 2)
                {
                    //throw new MappleException("JobNotNeeded");
                }
                else if (result == 3)
                {
                    //throw new MappleExcetion("DPSTypeNotNeeded");
                }
                else
                {
                    //Bruh, if we get here computer brocky. Just throw "BrockyError"
                    //throw new MappleException("VeryBrockyNeedFixes(AndProbablyLotsOfLove)");
                }
            }
            else
            {
                //throw new MappleException("PlayerLimitExceeded");
            }
        }

        public void RemovePlayer(Player Player, int EventId)
        {
            Event pEvent = _eventRepository.Get(EventId);

            if (pEvent.Requirement.PlayerCount >= 0)
            {
                int result = _requirementService.RemovePlayer(Player, pEvent.Requirement);

                if (result == 0)
                {
                    //Success
                    _requirementRepository.AddOrUpdate(pEvent.Requirement);
                    //We do not remove the Player from the table, since it might be used by another event. Instead, a system will take care to check if a Player is in any event and remove that player
                    //if it is in no events.
                    _requirementRepository.Save();
                }
                else if (result == 1)
                {
                    throw new MapleException("PlayerNotFound");
                }
                else
                {
                    //Bruh, if we get here computer brocky. Just throw "BrockyError"
                    throw new MapleException("VeryBrockyNeedFixes(AndProbablyLotsOfLove)");
                }
            }
            else
            {
                throw new MapleException("PlayerNotFound");
            }
        }

        public Event UpdateEvent(UpdateEventIn NewEvent) 
        {

            int Id = NewEvent.EventId;

            Event PastEvent = _eventRepository.Get(Id);

            Event nEvent = new Event()
            {
                //Stuff that changes
                StartDate = NewEvent.StartDate,
                EndDate = NewEvent.EndDate,
                EventType = NewEvent.EventType,
                EventStatus = NewEvent.EventStatus,
                FightName = NewEvent.FightName,
                //Old stuff that doesn't change
                Id = PastEvent.Id,
                Requirement = PastEvent.Requirement,
                RequirementId = PastEvent.RequirementId,

            };

            _eventRepository.AddOrUpdate(nEvent);//Update event in database

            return nEvent;

            

        }

        public void CreateEvent(Event pEvent, EventRequirement pRequirement)
        {
            //Have to generate new Requirement and assign Ids

            _eventRepository.Add(pEvent);
            _requirementRepository.Add(pRequirement);

            _eventRepository.Save();

        }

        public void DeleteEvent(int EventId)
        {
            Event rEvent = _eventRepository.Get(EventId);

            foreach(Player Player in rEvent.Requirement.Players)
            {
                //First will go remove this player from the User
                _userService.RemovePlayer(Player.Id, Player.UserId);

                _playerService.RemovePlayer(Player);//Remove Player from database

            }
            //Now we just gotta remove Requirement and Event from DB
            _eventRepository.Remove(rEvent);
            _requirementRepository.Remove(rEvent.Requirement);
            _eventRepository.Save();
            _requirementRepository.Save();
        }

        public int GetIdOwner(int EventId)
        {
            return _eventRepository.Get(EventId).OwnerId;
        }

        public DisplayEventOut DisplayEvent(int EventId)
        {

            Event pEvent = _eventRepository.Get(EventId);

            List<PlayerButton> PlayerButtonList = _requirementService.DisplayEvent(pEvent.Requirement);//Create player button list in order (TANK,HEALER,DPS)

            DisplayEventOut result = new DisplayEventOut()
            {
                NameFight = pEvent.FightName,
                //Description = pEvent.Description,

                PlayerCount = pEvent.Requirement.PlayerCount,
                PlayerLimit = pEvent.Requirement.PlayerLimit,
                OnePlayerPerJob = pEvent.Requirement.OnePerJob,
                DutyComplete = false,//For now false by default

                PlayerButton = PlayerButtonList

            };

            return result;
        }

    }
}
