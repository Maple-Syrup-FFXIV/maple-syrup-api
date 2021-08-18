using maple_syrup_api.Models;
using maple_syrup_api.Dto;
using maple_syrup_api.Repositories.IRepository;
using maple_syrup_api.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using maple_syrup_api.Exceptions;

namespace maple_syrup_api.Services.Service
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository pPlayerRepository)
        {
            _playerRepository = pPlayerRepository;
        }

        public Player getPlayer(int Id)
        {
            return _playerRepository.Get(Id);
        }

        public List<Player> getPlayerListFromId(List<int> PlayerList)
        {
            List<Player> nList = new List<Player>();

            foreach (int PlayerId in PlayerList)
            {
                if (PlayerId != -1)
                {
                    nList.Add(_playerRepository.Get(PlayerId));
                }
            }
            return nList;
        }

        public void AddPlayer(Player Player)
        {
            _playerRepository.Add(Player);
            _playerRepository.Save();
        }

        public void RemovePlayer(Player Player)
        {
            _playerRepository.Remove(Player);
            _playerRepository.Save();
        }
    }
}
