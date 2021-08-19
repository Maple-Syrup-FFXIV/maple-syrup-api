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

        public void AddPlayer(Player player, int EventId, int UserId)
        {

            Event pEvent = _eventRepository.Get(EventId);

            if (pEvent == null) throw new MapleException("NoEventWithGivenId");

            if (pEvent.Requirement.PlayerCount < pEvent.Requirement.PlayerLimit)
            {
                int result = _requirementService.AddPlayer(player, pEvent.Requirement);

                if (result == 0)
                {
                    //Success
                    //We will change the EventRequirement accordingly and save it to the database

                    //The Requirements have already been changed, the only thing left is to add the Player to the list

                    player.EventRequirement = pEvent.Requirement;
                    player.User = _userService.GetUser(UserId);

                    _playerService.AddPlayer(player);

                    pEvent.Requirement.Players.Add(player);//Add player
                
                    _requirementRepository.AddOrUpdate(pEvent.Requirement);



                    _requirementRepository.Save();
                }
                else if (result == 1)
                {
                    throw new MapleException("ClassNotNeeded");
                }
                else if (result == 2)
                {
                    throw new MapleException("JobNotNeeded");
                }
                else if (result == 3)
                {
                    throw new MapleException("DPSTypeNotNeeded");
                }
                else
                {
                    //Bruh, if we get here computer brocky. Just throw "BrockyError"
                    throw new MapleException("VeryBrockyNeedFixes(AndProbablyLotsOfLove)");
                }
            }
            else
            {
                throw new MapleException("PlayerLimitExceeded");
            }
        }

        public void RemovePlayer(Player Player, int EventId)
        {
            Event pEvent = _eventRepository.Get(EventId);

            if (pEvent == null) throw new MapleException("NoEventWithGivenId");

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

            if (PastEvent == null) throw new MapleException("NoEventWithGivenId");

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
            if (rEvent == null) throw new MapleException("NoEventWithGivenId");
            int l = rEvent.Requirement.Players.Count;
            for(int i = 0;i<l;i++)
            {
                //First will go remove this player from the User
                //_userService.RemovePlayer(Player.Id, Player.UserId);
                Player Player = rEvent.Requirement.Players[0];
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

        public DisplayEventOut DisplayEventWithId(int EventId)
        {

            //Same as DiplayEvent, but requires Id of Event instead of actual Event (NOT THE ONE USED IN BROWSEEVENT)

            Event pEvent = _eventRepository.Get(EventId);

            if(pEvent == null)
            {
                throw new MapleException("NoEventWithGivenIdExist");
            }

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
        public DisplayEventOut DisplayEvent(Event pEvent)
        {

            if (pEvent == null)
            {
                throw new MapleException("NoEventWithGivenIdExist");
            }

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

        public List<DisplayEventOut> BrowseEvent(BrowseEventIn pInput)
        {

            List<Event> EventList = _eventRepository.GetAllFromStartDate(pInput.Time);


            //Reserach in DataBase
            if (pInput.OnePlayerPerJobCriteria)
            {
                EventList = EventList.Where(x => x.Requirement.OnePerJob == pInput.OnePlayerPerJob).ToList();
            }

            if (pInput.BlueMageCriteria)
            {
                EventList = EventList.Where(x => x.Requirement.AllowBlueMage == pInput.AllowBlueMage).ToList();
            }

            if (pInput.SpecificEventType)
            {
                EventList = EventList.Where(x => x.EventType == pInput.EventType).ToList();
            }

            if (pInput.MinILevelCriteria)
            {
                EventList = EventList.Where(x => x.Requirement.MinILevel == pInput.MinILevel).ToList();
            }

            if (pInput.SpecificFight)
            {
                EventList = EventList.Where(x => x.FightName == pInput.FightName).ToList();
            }

            if (pInput.LookingForSpecificClass)
            {
                foreach(int Class in pInput.ClassRestriction)
                {
                    EventList = EventList.Where(x => x.Requirement.ClassRequirement[Class] >= 1).ToList();
                }
            }

            if (pInput.LookingForSpecificJob)
            {
                foreach (int Job in pInput.ClassRestriction)
                {
                    EventList = EventList.Where(x => x.Requirement.ClassRequirement[Job] >= 1).ToList();
                }
            }

            //End of Research

            List<DisplayEventOut> DisplayEventList = new List<DisplayEventOut>();

            if(EventList.Count == 0)
            {
                throw new MapleException("NoEventWithSuchResearchFilterWasFound");
            }
            else
            {
                foreach(Event Event in EventList)
                {
                    var newEntry = DisplayEvent(Event);//Bruh should prob change that so it doesn't call recheck of Event in DB (since already loaded)
                    DisplayEventList.Add(newEntry);
                }
            }

            return DisplayEventList;

        }

    }
}
