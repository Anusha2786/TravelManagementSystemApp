using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Data;
using TravelManagementSystemApp.Models;

namespace TravelManagementSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddresesController : ControllerBase
    {
        private readonly TravelManagementSystemAppContext _context;

        public AddresesController(TravelManagementSystemAppContext context)
        {
            _context = context;
        }

        // GET: api/Addreses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Addreses>>> GetAddreses()
        {
            return await _context.Addreses.ToListAsync();
        }

        // GET: api/Addreses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Addreses>> GetAddreses(int id)
        {
            var addreses = await _context.Addreses.FindAsync(id);

            if (addreses == null)
            {
                return NotFound();
            }

            return addreses;
        }

        // PUT: api/Addreses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddreses(int id, Addreses addreses)
        {
            if (id != addreses.AddressID)
            {
                return BadRequest();
            }

            _context.Entry(addreses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddresesExists(id))
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

        // POST: api/Addreses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Addreses>> PostAddreses(Addreses addreses)
        {
            _context.Addreses.Add(addreses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddreses", new { id = addreses.AddressID }, addreses);
        }

        // DELETE: api/Addreses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddreses(int id)
        {
            var addreses = await _context.Addreses.FindAsync(id);
            if (addreses == null)
            {
                return NotFound();
            }

            _context.Addreses.Remove(addreses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddresesExists(int id)
        {
            return _context.Addreses.Any(e => e.AddressID == id);
        }
    }
}
