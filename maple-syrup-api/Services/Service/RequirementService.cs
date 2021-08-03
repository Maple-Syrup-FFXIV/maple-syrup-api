using maple_syrup_api.Models;
using maple_syrup_api.Repositories.IRepository;
using maple_syrup_api.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace maple_syrup_api.Services.Service
{
    public class RequirementService : IRequirementService
    {
        private readonly IRequirementRepository _requirementRepository;
        private readonly IEventRepository _eventRepository;

        public int AddPlayer(Player player, EventRequirement pRequirement)
        {
            
            if (pRequirement.ClassRequirement[player.Class] > 0)
            {
                //A spot if left. So we will check if other requirements are also met
                if (!pRequirement.PreciseJob)
                {
                    //We do not require only certain job
                    pRequirement.ClassRequirement[player.Class]--;
                    pRequirement.PlayerCount++;
                    //ADD PLAYER TO REQUIREMENT AND SAVE REQUIREMENT/EVENT
                    return 0;

                }
                else
                {
                    //If we do require OnePerJob
                    if(pRequirement.PerJobRequirement[player.Job] >= 1)
                    {
                        if((pRequirement.DPSRequiredByType) && (player.Class == 2))
                        {
                            //If is a DPS and we have RequiredByType, then have to check if can add

                            if (pRequirement.DPSTypeRequirement[(int) player.DPSType] >= 1)
                            {
                                //Then we all set
                                pRequirement.PerJobRequirement[player.Job]--;
                                pRequirement.ClassRequirement[player.Class]--;
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
                            pRequirement.PerJobRequirement[player.Job]--;
                            pRequirement.ClassRequirement[player.Class]--;
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

        public int RemovePlayer(String PlayerName, EventRequirement pRequirement)
        {
            int l = pRequirement.Players.Count;
            for (int i = 0; i < l; i++)
            {
                if (PlayerName == pRequirement.Players[i].PlayerName) 
                {

                    Player removedPlayer = pRequirement.Players[i];

                    pRequirement.ClassRequirement[removedPlayer.Class]++;//Add a new spot for this class
                    if (pRequirement.PreciseJob) pRequirement.PerJobRequirement[removedPlayer.Job]++;//Add a new spot for this job if PreciseJob is true
                    if (pRequirement.DPSRequiredByType && (removedPlayer.Class == 2)) pRequirement.DPSTypeRequirement[(int) removedPlayer.DPSType]++;//Add a new spot for DPSType if DPSType is true and player is a DPS

                    pRequirement.Players[i] = null;//removes player object from list
                    pRequirement.PlayerCount--;
                    return 0;
                }
            }
            return 1;

        }

        public int UpdateRequirement(EventRequirement NewRequirement, Event pEvent)
        {
            //In order to check if the new requirement will fit the present player list, we will
            //add 1 by 1 the players to the NewRequirement and if an error ever comes up then we know it won't work

            List<Player> PresentList = pEvent.Requirement.Players;

            foreach (Player Player in PresentList)
            {
                int result = AddPlayer(Player, NewRequirement);//Add player to the new requirement set

                if (result != 0)
                {
                    //In this case an error occured.
                    return result;
                }
                else
                {
                    PresentList.Add(Player);
                    //Add takes care of updating counterList
                }

            }

            //If we get here, NewRequirement has been filled will all PLayers from pEvent. So we simply reassign the fields of pEvent and save into the database the new requirement/Event

            pEvent.Requirement = NewRequirement;
            _requirementRepository.AddOrUpdate(NewRequirement);
            _eventRepository.AddOrUpdate(pEvent);
            //The NewRequirement will have the same Id as the old one
            return 0;


        }


    }
}
