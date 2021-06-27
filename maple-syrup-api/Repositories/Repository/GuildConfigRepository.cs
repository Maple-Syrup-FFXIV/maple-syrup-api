using maple_syrup_api.Context;
using maple_syrup_api.Dto;
using maple_syrup_api.Models;
using maple_syrup_api.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
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

        public bool EditGuildConfig(EditGuildConfigIn pGuildConfig)
        {

            if (!GuildConfigExists(pGuildConfig.GuildId))
                return false;
            GuildConfig guild = new GuildConfig()
            {
                Id = _context.GuildConfigs.AsNoTracking().FirstOrDefault(x => x.GuildId == pGuildConfig.GuildId).Id,
                Prefix = pGuildConfig.Prefix,
                GuildId = pGuildConfig.GuildId
            };
            _context.Entry(guild).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
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
            return _context.GuildConfigs.AsNoTracking().Any(x => x.GuildId == pGuildID);
        }
    }
}
