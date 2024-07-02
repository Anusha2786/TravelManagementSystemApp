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
    public class RoomsController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext _context;

        public RoomsController(Hasslefreetraveldbcontext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all rooms.
        /// </summary>
        /// <returns>A list of Room objects</returns>
        /// <response code="200">Returns the list of rooms</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP 200 - OK
        public async Task<ActionResult<IEnumerable<Rooms>>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific room by ID.
        /// </summary>
        /// <param name="id">The ID of the room to retrieve</param>
        /// <returns>The room with the specified ID</returns>
        /// <response code="200">Returns the room</response>
        /// <response code="404">If a room with the specified ID is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP 200 - OK
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<ActionResult<Rooms>> GetRooms(int id)
        {
            var rooms = await _context.Rooms.FindAsync(id);

            if (rooms == null)
            {
                return NotFound();
            }

            return rooms;
        }

        /// <summary>
        /// Updates an existing room.
        /// </summary>
        /// <param name="id">The ID of the room to update</param>
        /// <param name="room">The updated room data</param>
        /// <returns>No content</returns>
        /// <response code="204">Indicates successful update</response>
        /// <response code="400">If the request data is invalid or the ID does not match</response>
        /// <response code="404">If the room with the specified ID does not exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // HTTP 400 - Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<IActionResult> PutRooms(int id, Rooms rooms)
        {
            if (id != rooms.RoomID)
            {
                return BadRequest();
            }

            _context.Entry(rooms).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomsExists(id))
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
        /// Creates a new room.
        /// </summary>
        /// <param name="room">The room data to create</param>
        /// <returns>A newly created room</returns>
        /// <response code="201">Returns the newly created room</response>
        /// <response code="400">If the room data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] // HTTP 201 - Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // HTTP 400 - Bad Request
        public async Task<ActionResult<Rooms>> PostRooms(Rooms rooms)
        {
            _context.Rooms.Add(rooms);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRooms", new { id = rooms.RoomID }, rooms);
        }

        /// <summary>
        /// Deletes a specific room by ID.
        /// </summary>
        /// <param name="id">The ID of the room to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">Returns when the room is successfully deleted</response>
        /// <response code="404">If no room with the specified ID exists</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<IActionResult> DeleteRooms(int id)
        {
            var rooms = await _context.Rooms.FindAsync(id);
            if (rooms == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(rooms);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomsExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomID == id);
        }
    }
}
