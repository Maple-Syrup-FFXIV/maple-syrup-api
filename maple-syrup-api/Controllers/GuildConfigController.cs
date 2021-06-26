using maple_syrup_api.Context;
using maple_syrup_api.Models;
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
        private readonly MapleSyrupContext _context;

        public GuildConfigController(MapleSyrupContext context)
        {
            _context = context;
        }

        // GET: api/GuildConfig
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuildConfig>>> GetGuildConfig()
        {
            return await _context.GuildConfigs.ToListAsync();
        }

        // GET: api/GuildConfig/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GuildConfig>> GetGuildConfig(int id)
        {
            var guildConfig = await _context.GuildConfigs.FindAsync(id);

            if (guildConfig == null)
            {
                return NotFound();
            }

            return guildConfig;
        }

        // PUT: api/GuildConfig/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuildConfig(int id, GuildConfig guildConfig)
        {
            if (id != guildConfig.Id)
            {
                return BadRequest();
            }

            _context.Entry(guildConfig).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuildConfigExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GuildConfig
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GuildConfig>> PostGuildConfig(GuildConfig guildConfig)
        {
            _context.GuildConfigs.Add(guildConfig);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGuildConfig", new { id = guildConfig.Id }, guildConfig);
        }

        // DELETE: api/GuildConfig/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuildConfig(int id)
        {
            var guildConfig = await _context.GuildConfigs.FindAsync(id);
            if (guildConfig == null)
            {
                return NotFound();
            }

            _context.GuildConfigs.Remove(guildConfig);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GuildConfigExists(int id)
        {
            return _context.GuildConfigs.Any(e => e.Id == id);
        }
    }
}
