using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Data;
using TravelManagementSystemApp.Models;

namespace TravelManagementSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainshedulesController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext context;
        private readonly ILogger<UsersController> logger;

        public TrainshedulesController(Hasslefreetraveldbcontext usersDbContext, ILogger<UsersController> logger)
        {
            context = usersDbContext;
            this.logger = logger;
        }
        /// <summary>
        /// Retrieves a list of all train schedules.
        /// </summary>
        /// <returns>A list of TrainSchedules objects</returns>
        /// <response code="200">Returns the list of train schedules</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP 200 - OK
        public async Task<ActionResult<IEnumerable<Trainschedules>>> Gettrain()
        {
            return await context.trainschedules.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific train schedule by ID.
        /// </summary>
        /// <param name="id">The ID of the train schedule to retrieve</param>
        /// <returns>A single TrainSchedules object</returns>
        /// <response code="200">Returns the train schedule with the specified ID</response>
        /// <response code="404">If no train schedule with the specified ID exists</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP 200 - OK
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<ActionResult<Trainschedules>> Getbyid(int id)
        {
            var train = await context.trainschedules.FindAsync(id);

            if (train == null)
            {
                return NotFound();
            }

            return train;
        }

        /// <summary>
        /// Creates a new train schedule.
        /// </summary>
        /// <param name="trainSchedule">The train schedule data to create</param>
        /// <returns>A newly created TrainSchedule object</returns>
        /// <response code="201">Returns the newly created train schedule</response>
        /// <response code="400">If the request data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] // HTTP 201 - Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // HTTP 400 - Bad Request

        public async Task<ActionResult<Trainschedules>> AddTrain(Trainschedules trainschedules)
        {
            try
            {
                context.trainschedules.Add(trainschedules);
                await context.SaveChangesAsync();
                return CreatedAtAction("Getbyid", new { id = trainschedules.TrainId }, trainschedules);
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "Error adding train schedule to database");
                return StatusCode(500, "Error adding train schedule to database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding train schedule");
                return StatusCode(500, "Error adding train schedule");
            }
        }

        /// <summary>
        /// Updates an existing train schedule.
        /// </summary>
        /// <param name="id">The ID of the train schedule to update</param>
        /// <param name="trainSchedule">The updated train schedule data</param>
        /// <returns>No content</returns>
        /// <response code="204">Indicates successful update</response>
        /// <response code="400">If the request data is invalid or the ID does not match</response>
        /// <response code="404">If the train schedule with the specified ID does not exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // HTTP 400 - Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<IActionResult> UpdateTrain(int id, Trainschedules trainschedules)
        {
            if (id != trainschedules.TrainId)
            {
                return BadRequest();
            }

            try
            {
                context.Entry(trainschedules).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.LogError(ex, "Error updating train schedule in database");
                return StatusCode(500, "Error updating train schedule in database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating train schedule");
                return StatusCode(500, "Error updating train schedule");
            }
        }

        /// <summary>
        /// Deletes a specific train schedule by ID.
        /// </summary>
        /// <param name="id">The ID of the train schedule to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">Indicates successful deletion</response>
        /// <response code="404">If the train schedule with the specified ID is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<ActionResult<Trainschedules>> DeleteTrain(int id)
        {
            var train = await context.trainschedules.FindAsync(id);

            if (train == null)
            {
                return NotFound();
            }

            try
            {
                context.trainschedules.Remove(train);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "Error deleting train schedule from database");
                return StatusCode(500, "Error deleting train schedule from database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting train schedule");
                return StatusCode(500, "Error deleting train schedule");
            }
        }
    }
}