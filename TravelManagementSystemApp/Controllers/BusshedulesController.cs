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
        private readonly UsersDbContext context;
        private readonly ILogger<UsersController> logger;

        public BusSchedulesController(UsersDbContext usersDbContext, ILogger<UsersController> logger)
        {
            context = usersDbContext;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Busschedules>>> Getbus()
        {
            return await context.busschedules.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Busschedules>> Getbyid(int id)
        {
            var bus = await context.busschedules.FindAsync(id);

            if (bus == null)
            {
                return NotFound();
            }

            return bus;
        }

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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