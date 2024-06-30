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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flightschedules>>> Getflight()
        {
            return await hasslefreetraveldbcontext.flightschedules.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Flightschedules>> Getbyid(int id)
        {
            var flight = await hasslefreetraveldbcontext.flightschedules.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
   
