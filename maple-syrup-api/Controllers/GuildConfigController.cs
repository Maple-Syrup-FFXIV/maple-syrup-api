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

        
    }
}
