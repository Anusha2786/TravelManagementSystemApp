using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Data;
using TravelManagementSystemApp.Models;

namespace TravelManagementSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightSchedulesController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext hasslefreetraveldbcontext;
        private readonly ILogger<UsersController> logger;

        public FlightSchedulesController(Hasslefreetraveldbcontext hasslefreetraveldbcontext, ILogger<UsersController> logger)
        {

            this.hasslefreetraveldbcontext = hasslefreetraveldbcontext;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves a list of all flight schedules.
        /// </summary>
        /// <returns>A list of FlightSchedule objects</returns>
        /// <response code="200">Returns the list of flight schedules</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP 200 - OK
        public async Task<ActionResult<IEnumerable<Flightschedules>>> Getflight()
        {
            return await hasslefreetraveldbcontext.flightschedules.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific flight schedule by ID.
        /// </summary>
        /// <param name="id">The ID of the flight schedule to retrieve</param>
        /// <returns>The flight schedule with the specified ID</returns>
        /// <response code="200">Returns the flight schedule</response>
        /// <response code="404">If a flight schedule with the specified ID is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP 200 - OK
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<ActionResult<Flightschedules>> Getbyid(int id)
        {
            var flight = await hasslefreetraveldbcontext.flightschedules.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }

        /// <summary>
        /// Creates a new flight schedule.
        /// </summary>
        /// <param name="flightSchedule">The flight schedule data to create</param>
        /// <returns>A newly created flight schedule</returns>
        /// <response code="201">Returns the newly created flight schedule</response>
        /// <response code="400">If the flight schedule data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] // HTTP 201 - Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // HTTP 400 - Bad Request
        public async Task<ActionResult<Flightschedules>> AddFlight(Flightschedules flightschedules)
        {
            try
            {
                hasslefreetraveldbcontext.flightschedules.Add(flightschedules);
                await hasslefreetraveldbcontext.SaveChangesAsync();
                return CreatedAtAction("Getbyid", new { id = flightschedules.FlightId }, flightschedules);
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "Error adding flight schedule to database");
                return StatusCode(500, "Error adding flight schedule to database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding flight schedule");
                return StatusCode(500, "Error adding flight schedule");
            }
        }


        /// <summary>
        /// Updates an existing flight schedule.
        /// </summary>
        /// <param name="id">The ID of the flight schedule to update</param>
        /// <param name="flightSchedule">The updated flight schedule data</param>
        /// <returns>No content</returns>
        /// <response code="204">Indicates successful update</response>
        /// <response code="400">If the request data is invalid or the ID does not match</response>
        /// <response code="404">If the flight schedule with the specified ID does not exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // HTTP 400 - Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<IActionResult> UpdateFlight(int id, Flightschedules flightschedules)
        {
            if (id != flightschedules.FlightId)
            {
                return BadRequest();
            }

            try
            {
                hasslefreetraveldbcontext.Entry(flightschedules).State = EntityState.Modified;
                await hasslefreetraveldbcontext.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.LogError(ex, "Error updating flight schedule in database");
                return StatusCode(500, "Error updating flight schedule in database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating flight schedule");
                return StatusCode(500, "Error updating flight schedule");
            }
        }


        /// <summary>
        /// Deletes a specific flight schedule by ID.
        /// </summary>
        /// <param name="id">The ID of the flight schedule to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">Returns when the flight schedule is successfully deleted</response>
        /// <response code="404">If no flight schedule with the specified ID exists</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<ActionResult<Flightschedules>> DeleteFlight(int id)
        {
            var flight = await hasslefreetraveldbcontext.flightschedules.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            try
            {
                hasslefreetraveldbcontext.flightschedules.Remove(flight);
                await hasslefreetraveldbcontext.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "Error deleting flight schedule from database");
                return StatusCode(500, "Error deleting flight schedule from database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting flight schedule");
                return StatusCode(500, "Error deleting flight schedule");
            }
        }
    }
    
    }
   
