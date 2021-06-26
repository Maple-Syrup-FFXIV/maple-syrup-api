using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using maple_syrup_api.Context;
using maple_syrup_api.Models;

namespace maple_syrup_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GuildConfigsController : ControllerBase
    {
        private readonly MapleSyrupContext _context;

        public GuildConfigsController(MapleSyrupContext context)
        {
            _context = context;
        }

        // GET: api/GuildConfigs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuildConfigs>>> GetGuildConfigs()
        {
            return await _context.GuildConfigs.ToListAsync();
        }

        // GET: api/GuildConfigs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GuildConfigs>> GetGuildConfigs(int id)
        {
            var guildConfigs = await _context.GuildConfigs.FindAsync(id);

            if (guildConfigs == null)
            {
                return NotFound();
            }

            return guildConfigs;
        }

        // PUT: api/GuildConfigs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuildConfigs(int id, GuildConfigs guildConfigs)
        {
            if (id != guildConfigs.Id)
            {
                return BadRequest();
            }

            _context.Entry(guildConfigs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuildConfigsExists(id))
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

        // POST: api/GuildConfigs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GuildConfigs>> PostGuildConfigs(GuildConfigs guildConfigs)
        {
            _context.GuildConfigs.Add(guildConfigs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGuildConfigs", new { id = guildConfigs.Id }, guildConfigs);
        }

        // DELETE: api/GuildConfigs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuildConfigs(int id)
        {
            var guildConfigs = await _context.GuildConfigs.FindAsync(id);
            if (guildConfigs == null)
            {
                return NotFound();
            }

            _context.GuildConfigs.Remove(guildConfigs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GuildConfigsExists(int id)
        {
            return _context.GuildConfigs.Any(e => e.Id == id);
        }
    }
}
