using maple_syrup_api.Context;
using maple_syrup_api.Dto;
using maple_syrup_api.Models;
using maple_syrup_api.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Repositories.Repository
{
    public class GuildConfigRepository : BaseRepository<GuildConfig>, IGuildConfigRepository
    {
        private readonly MapleSyrupContext _context;

        public GuildConfigRepository(MapleSyrupContext pContext): base(pContext)
        {
            _context = pContext;
        }
        public GuildConfig AddGuildConfig(AddGuildConfigIn pGuildConfig)
        {
            GuildConfig guild = new GuildConfig()
            {
                Prefix = pGuildConfig.Prefix,
                GuildId = pGuildConfig.GuildId
            };
            if (GuildConfigExists(guild.GuildId))
            {
                return null;
            }
            var query = _context.GuildConfigs.Add(guild);
            _context.SaveChangesAsync();
            return guild;
            
        }

        public void EditGuildConfig(EditGuildConfigIn pGuildConfig)
        {
            throw new NotImplementedException();
        }

        public GuildConfig GetGuildConfig(string pGuildID)
        {
            var result = _context.GuildConfigs.FirstOrDefault(x => x.GuildId == pGuildID);

            return result;
        }

        public List<GuildConfig> GetGuildConfigs()
        {
            var result = _context.GuildConfigs.ToList();

            return result;
        }

        private bool GuildConfigExists(string pGuildID)
        {
            return _context.GuildConfigs.Any(x => x.GuildId == pGuildID);
        }
    }
}
