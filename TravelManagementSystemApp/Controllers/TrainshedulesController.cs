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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trainschedules>>> Gettrain()
        {
            return await context.trainschedules.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Trainschedules>> Getbyid(int id)
        {
            var train = await context.trainschedules.FindAsync(id);

            if (train == null)
            {
                return NotFound();
            }

            return train;
        }

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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