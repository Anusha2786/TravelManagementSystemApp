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
    public class StationsController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext _context;

        public StationsController(Hasslefreetraveldbcontext context)
        {
            _context = context;
        }

        // GET: api/Stations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stations>>> GetStations()
        {
            return await _context.Stations.ToListAsync();
        }

        // GET: api/Stations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stations>> GetStations(int id)
        {
            var stations = await _context.Stations.FindAsync(id);

            if (stations == null)
            {
                return NotFound();
            }

            return stations;
        }

        // PUT: api/Stations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStations(int id, Stations stations)
        {
            if (id != stations.StationID)
            {
                return BadRequest();
            }

            _context.Entry(stations).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationsExists(id))
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

        // POST: api/Stations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stations>> PostStations(Stations stations)
        {
            _context.Stations.Add(stations);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStations", new { id = stations.StationID }, stations);
        }

        // DELETE: api/Stations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStations(int id)
        {
            var stations = await _context.Stations.FindAsync(id);
            if (stations == null)
            {
                return NotFound();
            }

            _context.Stations.Remove(stations);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StationsExists(int id)
        {
            return _context.Stations.Any(e => e.StationID == id);
        }
    }
}
