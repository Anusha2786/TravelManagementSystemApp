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
    public class HotelsController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext _context;

        public HotelsController(Hasslefreetraveldbcontext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all hotels.
        /// </summary>
        /// <returns>A list of Hotel objects</returns>
        /// <response code="200">Returns the list of hotels</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP 200 - OK
        public async Task<ActionResult<IEnumerable<Hotels>>> GetHotels()
        {
            return await _context.Hotels.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific hotel by ID.
        /// </summary>
        /// <param name="id">The ID of the hotel to retrieve</param>
        /// <returns>The hotel with the specified ID</returns>
        /// <response code="200">Returns the hotel</response>
        /// <response code="404">If a hotel with the specified ID is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP 200 - OK
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<ActionResult<Hotels>> GetHotels(int id)
        {
            var hotels = await _context.Hotels.FindAsync(id);

            if (hotels == null)
            {
                return NotFound();
            }

            return hotels;
        }

        /// <summary>
        /// Updates an existing hotel.
        /// </summary>
        /// <param name="id">The ID of the hotel to update</param>
        /// <param name="hotel">The updated hotel data</param>
        /// <returns>No content</returns>
        /// <response code="204">Indicates successful update</response>
        /// <response code="400">If the request data is invalid or the ID does not match</response>
        /// <response code="404">If the hotel with the specified ID does not exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // HTTP 400 - Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<IActionResult> PutHotels(int id, Hotels hotels)
        {
            if (id != hotels.HotelID)
            {
                return BadRequest();
            }

            _context.Entry(hotels).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelsExists(id))
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
        /// Creates a new hotel.
        /// </summary>
        /// <param name="hotel">The hotel data to create</param>
        /// <returns>A newly created hotel</returns>
        /// <response code="201">Returns the newly created hotel</response>
        /// <response code="400">If the hotel data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] // HTTP 201 - Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // HTTP 400 - Bad Request
        public async Task<ActionResult<Hotels>> PostHotels(Hotels hotels)
        {
            _context.Hotels.Add(hotels);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotels", new { id = hotels.HotelID }, hotels);
        }

        /// <summary>
        /// Deletes a specific hotel by ID.
        /// </summary>
        /// <param name="id">The ID of the hotel to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">Returns when the hotel is successfully deleted</response>
        /// <response code="404">If no hotel with the specified ID exists</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<IActionResult> DeleteHotels(int id)
        {
            var hotels = await _context.Hotels.FindAsync(id);
            if (hotels == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotels);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelsExists(int id)
        {
            return _context.Hotels.Any(e => e.HotelID == id);
        }
    }
}
