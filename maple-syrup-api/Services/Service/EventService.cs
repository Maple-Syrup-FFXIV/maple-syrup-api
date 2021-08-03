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
        private readonly IRequirementRepository _requirementRepository;

        public EventService(IEventRepository pEventRepository)
        {
            _eventRepository = pEventRepository;
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

        public void AddPlayer(Player player, Event pEvent)
        {



            if (pEvent.Requirement.PlayerLimit >= 1)
            {
                int result = RequirementService.AddPlayer(player, pEvent.Requirement);

                if (result == 0)
                {
                    //Success
                    //We will change the EventRequirement accordingly and save it to the database

                    //The Requirements have already been changed, the only thing left is to add the Player to the list

                    pEvent.Requirement.Players.Add(player);//Add player
                    pEvent.Requirement.PlayerLimit--;//Update Player count

                    _requirementRepository.AddOrUpdate(pEvent.Requirement);

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

        public void RemovePlayer(String PlayerName, Event pEvent)
        {

            if (pEvent.Requirement.Players.Count != 0)
            {
                int result = RequirementService.RemovePlayer(PlayerName, pEvent.Requirement);

                if (result == 0)
                {
                    //Success
                    _requirementRepository.AddOrUpdate(pEvent.Requirement);
                }
                else if (result == 1)
                {
                    //throw new MappleException("PlayerNotFound");
                }
                else
                {
                    //Bruh, if we get here computer brocky. Just throw "BrockyError"
                    //throw new MappleException("VeryBrockyNeedFixes(AndProbablyLotsOfLove)");
                }
            }
            else
            {
                //throw new MappleException("CannotRemoveFromEmptyList");
            }
        }

    }
}
