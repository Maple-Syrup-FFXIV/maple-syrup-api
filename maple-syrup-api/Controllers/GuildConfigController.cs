using maple_syrup_api.Context;
using maple_syrup_api.Dto;
using maple_syrup_api.Models;
using maple_syrup_api.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace maple_syrup_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GuildConfigController : ControllerBase
    {
        private readonly IGuildConfigService _guildService;
        public GuildConfigController(IGuildConfigService pGuildConfigService)
        {
            _guildService = pGuildConfigService;
        }

        // GET: api/GuildConfig
        [HttpGet]
        public async Task<ActionResult<GetGuildConfigsOut>> GetGuildConfigs()
        {
            var result = new GetGuildConfigsOut();
            var guildList = _guildService.GetGuildConfigs();

            result = new GetGuildConfigsOut()
            {
                GuildList = guildList.Select(s => new PartialGuildGetGuildConfigs()
                {
                    Id = s.Id,
                    Prefix = s.Prefix,
                    GuildId = s.GuildId
                }).ToList()
            };
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<GetGuildConfigOut>> GetGuildConfig(GetGuildConfigIn pGuildConfig)
        {
            var result = new GetGuildConfigOut();
            var guild = _guildService.GetGuildConfig(pGuildConfig.GuildId);

            result = new GetGuildConfigOut
            {
                Id = guild.Id,
                Prefix = guild.Prefix,
                GuildId = guild.GuildId
            };

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<AddGuildConfigOut>> AddGuildConfig(AddGuildConfigIn pGuildConfig)
        {
            var result = new AddGuildConfigOut();
            var guild = _guildService.AddGuildConfig(pGuildConfig);

            if (guild is null)
                return BadRequest();

            result = new AddGuildConfigOut()
            {
                Id = guild.Id,
                Prefix = guild.Prefix,
                GuildId = guild.GuildId
            };
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> EditGuildConfig(EditGuildConfigIn pGuildConfig)
        {
            var result = _guildService.EditGuildConfig(pGuildConfig);

            if (!result)
                return NotFound();

            return NoContent();
        }
        
    }
}
