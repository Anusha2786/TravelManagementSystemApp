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

        /// <summary>
        /// Retrieves all airports.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/airports
        /// </remarks>
        /// <returns>An array of Airport objects</returns>
        /// <response code="200">Returns the list of airports</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Airports>>> GetAirports()
        {
            var airports = await _context.Airports.Include(a => a.Address).ToListAsync();
            return airports;
        }


        /// <summary>
        /// Retrieves a specific airport by ID.
        /// </summary>
        /// <param name="id">The ID of the airport to retrieve</param>
        /// <returns>A single Airport object</returns>
        /// <response code="200">Returns the airport with the specified ID</response>
        /// <response code="404">If no airport with the specified ID exists</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Airports>> GetAirports(int id)
        {
            var airports = await _context.Airports.FindAsync(id);

            if (airports == null)
            {
                return NotFound();
            }

            return airports;
        }

        /// <summary>
        /// Updates an existing airport by ID.
        /// </summary>
        /// <param name="id">The ID of the airport to update</param>
        /// <param name="airport">The updated airport object</param>
        /// <returns>No content</returns>
        /// <response code="204">If the airport was successfully updated</response>
        /// <response code="400">If the request data is invalid</response>
        /// <response code="404">If no airport with the specified ID exists</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Creates a new airport.
        /// </summary>
        /// <param name="airport">The airport object to create</param>
        /// <returns>A newly created airport</returns>
        /// <response code="201">Returns the newly created airport</response>
        /// <response code="400">If the request data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Airports>> PostAirports(Airports airports)
        {
            _context.Airports.Add(airports);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAirports", new { id = airports.AirportID }, airports);
        }

        /// <summary>
        /// Deletes a specific airport by ID.
        /// </summary>
        /// <param name="id">The ID of the airport to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">If the airport is successfully deleted</response>
        /// <response code="404">If no airport with the specified ID exists</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
