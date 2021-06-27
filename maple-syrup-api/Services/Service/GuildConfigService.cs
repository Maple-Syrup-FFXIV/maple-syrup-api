using maple_syrup_api.Dto;
using maple_syrup_api.Models;
using maple_syrup_api.Repositories.IRepository;
using maple_syrup_api.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Services.Service
{
    public class GuildConfigService : IGuildConfigService
    {
        private readonly IGuildConfigRepository _guildRepository;
        public GuildConfigService(IGuildConfigRepository pGuildConfigRepository)
        {
            _guildRepository = pGuildConfigRepository;
        }
        public GuildConfig AddGuildConfig(AddGuildConfigIn pGuildConfig)
        {
            return _guildRepository.AddGuildConfig(pGuildConfig);
        }

        public void EditGuildConfig(EditGuildConfigIn pGuildConfig)
        {
            _guildRepository.EditGuildConfig(pGuildConfig);
        }

        public GuildConfig GetGuildConfig(string pGuildID)
        {
            return _guildRepository.GetGuildConfig(pGuildID);
        }

        public List<GuildConfig> GetGuildConfigs()
        {
            return _guildRepository.GetGuildConfigs();
        }
    }
}
