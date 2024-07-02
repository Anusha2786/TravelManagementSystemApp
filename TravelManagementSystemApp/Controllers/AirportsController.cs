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
    public class AirportsController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext _context;

        public AirportsController(Hasslefreetraveldbcontext context)
        {
            _context = context;
        }

        // GET: api/Airports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Airports>>> GetAirports()
        {
            return await _context.Airports.ToListAsync();
        }

        // GET: api/Airports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Airports>> GetAirports(int id)
        {
            var airports = await _context.Airports.FindAsync(id);

            if (airports == null)
            {
                return NotFound();
            }

            return airports;
        }

        // PUT: api/Airports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAirports(int id, Airports airports)
        {
            if (id != airports.AirportID)
            {
                return BadRequest();
            }

            _context.Entry(airports).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirportsExists(id))
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

        // POST: api/Airports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Airports>> PostAirports(Airports airports)
        {
            _context.Airports.Add(airports);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAirports", new { id = airports.AirportID }, airports);
        }

        // DELETE: api/Airports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirports(int id)
        {
            var airports = await _context.Airports.FindAsync(id);
            if (airports == null)
            {
                return NotFound();
            }

            _context.Airports.Remove(airports);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AirportsExists(int id)
        {
            return _context.Airports.Any(e => e.AirportID == id);
        }
    }
}
