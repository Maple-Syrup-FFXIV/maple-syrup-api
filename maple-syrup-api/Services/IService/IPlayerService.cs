using maple_syrup_api.Models;
using maple_syrup_api.Repositories.IRepository;
using maple_syrup_api.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using maple_syrup_api.Dto;

namespace maple_syrup_api.Services.IService
{
    public interface IPlayerService
    {
        Player getPlayer(int Id);
        List<Player> getPlayerListFromId(List<int> PlayerList);

        Player AddPlayer(RequirementAddPlayerIn Player);

        void RemovePlayer(Player Player);
    }
}
