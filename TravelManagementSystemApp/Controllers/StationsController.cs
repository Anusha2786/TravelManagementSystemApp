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

        /// <summary>
        /// Retrieves a list of all stations.
        /// </summary>
        /// <returns>A list of Station objects</returns>
        /// <response code="200">Returns the list of stations</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP 200 - OK
        public async Task<ActionResult<IEnumerable<Stations>>> GetStations()
        {
            return await _context.Stations.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific station by ID.
        /// </summary>
        /// <param name="id">The ID of the station to retrieve</param>
        /// <returns>The station with the specified ID</returns>
        /// <response code="200">Returns the station</response>
        /// <response code="404">If a station with the specified ID is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP 200 - OK
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<ActionResult<Stations>> GetStations(int id)
        {
            var stations = await _context.Stations.FindAsync(id);

            if (stations == null)
            {
                return NotFound();
            }

            return stations;
        }

        /// <summary>
        /// Updates an existing station.
        /// </summary>
        /// <param name="id">The ID of the station to update</param>
        /// <param name="station">The updated station data</param>
        /// <returns>No content</returns>
        /// <response code="204">Indicates successful update</response>
        /// <response code="400">If the request data is invalid or the ID does not match</response>
        /// <response code="404">If the station with the specified ID does not exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // HTTP 400 - Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
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

        /// <summary>
        /// Creates a new station.
        /// </summary>
        /// <param name="station">The station data to create</param>
        /// <returns>A newly created station</returns>
        /// <response code="201">Returns the newly created station</response>
        /// <response code="400">If the station data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] // HTTP 201 - Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // HTTP 400 - Bad Request
        public async Task<ActionResult<Stations>> PostStations(Stations stations)
        {
            _context.Stations.Add(stations);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStations", new { id = stations.StationID }, stations);
        }
        /// <summary>
        /// Deletes a specific station by ID.
        /// </summary>
        /// <param name="id">The ID of the station to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">Indicates successful deletion</response>
        /// <response code="404">If the station with the specified ID is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
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
