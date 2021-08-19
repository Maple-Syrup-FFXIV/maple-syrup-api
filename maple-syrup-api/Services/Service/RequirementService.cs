using maple_syrup_api.Exceptions;
using maple_syrup_api.Models;
using maple_syrup_api.Repositories.IRepository;
using maple_syrup_api.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using maple_syrup_api.Dto.EventManagement;


namespace maple_syrup_api.Services.Service
{
    public class RequirementService : IRequirementService
    {
        private readonly IRequirementRepository _requirementRepository;
        private readonly IEventRepository _eventRepository;
        public RequirementService(IRequirementRepository pRequirementRepository,IEventRepository pEventRepository)
        {
            _requirementRepository = pRequirementRepository;
            _eventRepository = pEventRepository;
        }


        public int AddPlayer(Player player, EventRequirement pRequirement)
        {
            if (pRequirement.ClassRequirement[(int) player.Class] > 0)
            {
                //A spot if left. So we will check if other requirements are also met
                if (!pRequirement.PreciseJob)
                {
                    //We do not require only certain job
                    pRequirement.ClassRequirement[(int) player.Class]--;
                    pRequirement.PlayerCount++;
                    //ADD PLAYER TO REQUIREMENT AND SAVE REQUIREMENT/EVENT
                    return 0;

                }
                else
                {
                    //If we do require OnePerJob
                    if(pRequirement.PerJobRequirement[(int) player.Job] >= 1)
                    {
                        if((pRequirement.DPSRequiredByType) && ((int) player.Class == 2))
                        {
                            //If is a DPS and we have RequiredByType, then have to check if can add

                            if (pRequirement.DPSTypeRequirement[(int) player.DPSType] >= 1)
                            {
                                //Then we all set
                                pRequirement.PerJobRequirement[(int) player.Job]--;
                                pRequirement.ClassRequirement[(int) player.Class]--;
                                pRequirement.DPSTypeRequirement[(int) player.DPSType]--;
                                pRequirement.PlayerCount++;
                                return 0;
                            }
                            else
                            {
                                //No more of this DPSType are needed
                                return 3;
                            }

                        }
                        else
                        {
                            pRequirement.PerJobRequirement[(int) player.Job]--;
                            pRequirement.ClassRequirement[(int) player.Class]--;
                            pRequirement.PlayerCount++;
                            return 0;
                        }
                    }
                    else
                    {
                        //Error : No more of this Job are needed
                        //throw new MappleException("NoMoreOfThisJobNeeded");
                        return 2;
                    }
                }
            }
            else
            {
                //Error : No more of their class was needed
                //throw new MappleException("NoMoreOfThisClassNeeded");
                return 1;
            }
        }

        public int RemovePlayer(Player Player, EventRequirement pRequirement)
        {
            int l = pRequirement.Players.Count;
            for (int i = 0; i < l; i++)
            {
                if (Player.Id == pRequirement.Players[i].Id) 
                {

                    pRequirement.ClassRequirement[(int)Player.Class]++;//Add a new spot for this class
                    if (pRequirement.PreciseJob) pRequirement.PerJobRequirement[(int)Player.Job]++;//Add a new spot for this job if PreciseJob is true
                    if (pRequirement.DPSRequiredByType && ((int)Player.Class == 2)) pRequirement.DPSTypeRequirement[(int)Player.DPSType]++;//Add a new spot for DPSType if DPSType is true and player is a DPS

                    pRequirement.Players.RemoveAt(i);//removes player object from list. -1 means no player
                    pRequirement.PlayerCount--;
                    return 0;
                }
            }
            return 1;

        }

        public int UpdateRequirement(EventRequirement NewRequirement, int EventId)
        {
            //In order to check if the new requirement will fit the present player list, we will
            //add 1 by 1 the players to the NewRequirement and if an error ever comes up then we know it won't work\
            Event pEvent = _eventRepository.Get(EventId);
            List<Player> PresentList = pEvent.Requirement.Players;
            foreach (Player Player in PresentList)
            {
                int result = AddPlayer(Player, NewRequirement);//Add player to the new requirement set

                if (result != 0)
                {
                    //In this case an error occured.
                    throw new MapleException("CouldNotUpdateRequirementWithGivenRequirements");
                }

            }
            //If it works get here


            pEvent.Requirement.PreciseJob = NewRequirement.PreciseJob;
            pEvent.Requirement.OnePerJob = NewRequirement.OnePerJob;
            pEvent.Requirement.DPSRequiredByType = NewRequirement.DPSRequiredByType;
            pEvent.Requirement.AllowBlueMage = NewRequirement.AllowBlueMage;
            pEvent.Requirement.DPSTypeRequirement = NewRequirement.DPSTypeRequirement;
            pEvent.Requirement.ClassRequirement = NewRequirement.ClassRequirement;
            pEvent.Requirement.PerJobRequirement = NewRequirement.PerJobRequirement;
            pEvent.Requirement.PlayerLimit = NewRequirement.PlayerLimit;
            pEvent.Requirement.MinILevel = NewRequirement.MinILevel;
            pEvent.Requirement.MinLevel = NewRequirement.MinLevel;
            pEvent.Requirement.OriginalClassRequirement = NewRequirement.ClassRequirement;
            pEvent.Requirement.OriginalPerJobRequirement = NewRequirement.PerJobRequirement;
            pEvent.Requirement.OriginalDPSTypeRequirement = NewRequirement.DPSTypeRequirement;


            //If we get here, NewRequirement has been filled will all PLayers from pEvent. So we simply reassign the fields of pEvent and save into the database the new requirement/Event
            _requirementRepository.AddOrUpdate(pEvent.Requirement);
            _requirementRepository.Save();
            //The NewRequirement will have the same Id as the old one
            return 0;


        }

        public EventRequirement GetRequirementWithPlayers(int pEventId){
            EventRequirement requirement = _eventRepository.GetEventWithPlayers(pEventId)?.Requirement;
            return requirement;
        }

        public EventRequirement GetRequirement(int EventId)
        {
            return _eventRepository.Get(EventId).Requirement;
        }

        public List<PlayerButton> DisplayEvent(EventRequirement pRequirement)
        {

            List<PlayerButton> resultList = new List<PlayerButton>();

            for(int i = 0;i<3; i++) 
            {
                //It will go through each TANK,HEALER and DPS requirement in this order, find the players and add PlayerButton to the list

                int count = 0;
                int l = pRequirement.OriginalClassRequirement[i];

                foreach(Player Player in pRequirement.Players)
                {
                    if((int) Player.Class == i)
                    {
                        resultList.Add(new PlayerButton()
                        {
                            Icon = (IconType) ((int) Player.Job + 6),
                            PlayerName = Player.PlayerName,
                            IsFree = false
                        }) ;
                        count++;
                    }
                }

                if(count < l)
                {
                    for(int k = 0; k < (l - count); k++)
                    {
                        resultList.Add(new PlayerButton
                        {
                            Icon = (IconType) (int) i,
                            PlayerName = "NoName",
                            IsFree = true
                        });
                    }
                }
                else if(count > l)
                {
                    throw new MapleException("BigErrorOccured. Counted more of a class than this event requires");
                }

                

            }

            return resultList;
        }

    }
}
