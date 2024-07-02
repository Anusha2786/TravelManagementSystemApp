using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Data;
using TravelManagementSystemApp.Models;
using TravelManagementSystemApp.Models.Entities;

namespace TravelManagementSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext context;
        private readonly ILogger<UsersController> logger;

        public UsersController(Hasslefreetraveldbcontext context, ILogger<UsersController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>A list of User objects</returns>
        /// <response code="200">Returns the list of users</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Users>))]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await context.users.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve</param>
        /// <returns>A single User object</returns>
        /// <response code="200">Returns the user with the specified ID</response>
        /// <response code="404">If no user with the specified ID exists</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Users))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user = await context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        /// <summary>
        /// Creates a new booking.
        /// </summary>
        
        /// <returns>The newly created Booking object</returns>
        /// <response code="201">Returns the newly created booking</response>
        /// <response code="400">If the request is invalid or booking creation fails</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Bookings))] // HTTP 201 - Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // HTTP 400 - Bad Request
        public async Task<ActionResult<Users>> PostUser(Users user)
        {
            try
            {
                context.users.Add(user);
                await context.SaveChangesAsync();
                return CreatedAtAction("GetUser", new { id = user.userid }, user);
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "Error adding user to database");
                return StatusCode(500, "Error adding user to database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding user");
                return StatusCode(500, "Error adding user");
            }
        }


        /// <summary>
        /// Updates an existing booking.
        /// </summary>
        /// <param name="id">The ID of the booking to update</param>
        /// <param name="bookingDTO">The updated booking data</param>
        /// <returns>No content response if successful</returns>
        /// <response code="204">No content response if the update is successful</response>
        /// <response code="400">If the request is invalid or booking update fails</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // HTTP 400 - Bad Request
        public async Task<IActionResult> PutUser(int id, Users user)
        {
            if (id != user.userid)
            {
                return BadRequest();
            }

            try
            {
                context.Entry(user).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.LogError(ex, "Error updating user in database");
                return StatusCode(500, "Error updating user in database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating user");
                return StatusCode(500, "Error updating user");
            }
        }

        /// <summary>
        /// Deletes a booking by ID.
        /// </summary>
        /// <param name="id">The ID of the booking to delete</param>
        /// <returns>No content response if successful</returns>
        /// <response code="204">No content response if the deletion is successful</response>
        /// <response code="404">If no booking with the specified ID exists</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<ActionResult<Users>> DeleteUser(int id)
        {
            var user = await context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            try
            {
                context.users.Remove(user);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "Error deleting user from database");
                return StatusCode(500, "Error deleting user from database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting user");
                return StatusCode(500, "Error deleting user");
            }
        }
    }
}