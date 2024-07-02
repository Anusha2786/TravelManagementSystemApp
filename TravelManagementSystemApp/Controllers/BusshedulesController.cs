using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Data;
using TravelManagementSystemApp.Models;

namespace TravelManagementSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusSchedulesController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext context;
        private readonly ILogger<UsersController> logger;

        public BusSchedulesController(Hasslefreetraveldbcontext usersDbContext, ILogger<UsersController> logger)
        {
            context = usersDbContext;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves a list of all bus schedules.
        /// </summary>
        /// <returns>A list of bus schedules</returns>
        /// <response code="200">Returns the list of bus schedules</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP 200 - OK
        public async Task<ActionResult<IEnumerable<Busschedules>>> Getbus()
        {
            return await context.busschedules.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific bus schedule by ID.
        /// </summary>
        /// <param name="id">The ID of the bus schedule to retrieve</param>
        /// <returns>The bus schedule with the specified ID</returns>
        /// <response code="200">Returns the bus schedule</response>
        /// <response code="404">If a bus schedule with the specified ID is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP 200 - OK
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<ActionResult<Busschedules>> Getbyid(int id)
        {
            var bus = await context.busschedules.FindAsync(id);

            if (bus == null)
            {
                return NotFound();
            }

            return bus;
        }

        /// <summary>
        /// Creates a new bus schedule.
        /// </summary>
        /// <param name="busSchedule">The bus schedule data to create</param>
        /// <returns>The newly created bus schedule</returns>
        /// <response code="201">Returns the newly created bus schedule</response>
        /// <response code="400">If the request data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] // HTTP 201 - Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // HTTP 400 - Bad Request
        public async Task<ActionResult<Busschedules>> AddBus(Busschedules busschedules)
        {
            try
            {
                context.busschedules.Add(busschedules);
                await context.SaveChangesAsync();
                return CreatedAtAction("Getbyid", new { id = busschedules.BusId }, busschedules);
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "Error adding bus schedule to database");
                return StatusCode(500, "Error adding bus schedule to database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding bus schedule");
                return StatusCode(500, "Error adding bus schedule");
            }
        }

        /// <summary>
        /// Updates an existing bus schedule.
        /// </summary>
        /// <param name="id">The ID of the bus schedule to update</param>
        /// <param name="busSchedule">The updated bus schedule data</param>
        /// <returns>No content</returns>
        /// <response code="204">Indicates successful update</response>
        /// <response code="400">If the request data is invalid or the ID does not match</response>
        /// <response code="404">If the bus schedule with the specified ID does not exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // HTTP 400 - Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<IActionResult> UpdateBus(int id, Busschedules busschedules)
        {
            if (id != busschedules.BusId)
            {
                return BadRequest();
            }

            try
            {
                context.Entry(busschedules).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.LogError(ex, "Error updating bus schedule in database");
                return StatusCode(500, "Error updating bus schedule in database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating bus schedule");
                return StatusCode(500, "Error updating bus schedule");
            }
        }

        /// <summary>
        /// Deletes a specific bus schedule by ID.
        /// </summary>
        /// <param name="id">The ID of the bus schedule to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">Indicates successful deletion</response>
        /// <response code="404">If the bus schedule with the specified ID does not exist</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<ActionResult<Busschedules>> DeleteBus(int id)
        {
            var bus = await context.busschedules.FindAsync(id);

            if (bus == null)
            {
                return NotFound();
            }

            try
            {
                context.busschedules.Remove(bus);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "Error deleting bus schedule from database");
                return StatusCode(500, "Error deleting bus schedule from database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting bus schedule");
                return StatusCode(500, "Error deleting bus schedule");
            }
        }
    }
}