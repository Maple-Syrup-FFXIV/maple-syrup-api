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
        private readonly IRequirementRepository _requirementRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;

        public PlayerService(IPlayerRepository pPlayerRepository, IUserRepository pUserRepository, IRequirementRepository pRequirementRepository, IEventRepository pEventRepository)
        {
            _playerRepository = pPlayerRepository;
            _requirementRepository = pRequirementRepository;
            _userRepository = pUserRepository;
            _eventRepository = pEventRepository;
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

        public Player AddPlayer(RequirementAddPlayerIn pInput)
        {
            Player Player = new Player()
            {
                UserId = pInput.UserId,
                EventRequirementId = pInput.EventId,

                PlayerName = pInput.PlayerName,

                Class = pInput.Class,
                Job = pInput.Job,
                DPSType = pInput.DPSType
            };

            Player.EventRequirement = _eventRepository.Get(pInput.EventId).Requirement;
            Player.User = _userRepository.Get(pInput.UserId);

            _playerRepository.Add(Player);
            _playerRepository.Save();
            return Player;
        }

        public void RemovePlayer(Player Player)
        {
            _playerRepository.Remove(Player);
            _playerRepository.Save();
        }
    }
}
