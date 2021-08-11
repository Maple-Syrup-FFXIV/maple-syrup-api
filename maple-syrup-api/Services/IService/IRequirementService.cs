using maple_syrup_api.Models;
using maple_syrup_api.Repositories.IRepository;
using maple_syrup_api.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Services.IService
{
    public interface IRequirementService
    {

        int AddPlayer(Player player, EventRequirement pRequirement);

        int RemovePlayer(String PlayerName, EventRequirement pRequirement);
        //Both these methods will add or remove a player from the requirements. It will return True or False depending on if the operation was
        //succesful. The Event will call these methods, and will take care of what to do if it returns true or false.

        //int UpdatePlayer(Player player, EventRequirement pRequirement);
        int UpdateRequirement(EventRequirement NewRequirement, int EventId);

        EventRequirement GetRequirement(int EventId);
    }
}
