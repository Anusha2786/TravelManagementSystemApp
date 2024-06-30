using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Data;
using TravelManagementSystemApp.Models.Entities;

namespace TravelManagementSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabsController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext hasslefreetraveldbcontext;
        public CabsController(Hasslefreetraveldbcontext hasslefreetraveldbcontext)
        {
            this.hasslefreetraveldbcontext = hasslefreetraveldbcontext;
        }
        // GET: api/Cabs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CabDTO>>> GetCabs()
        {
            var cabs = await hasslefreetraveldbcontext.Cabs.ToListAsync();
            return Ok(cabs);
        }
        // GET: api/Cabs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CabDTO>> GetCab(int id)
        {
            var bus = await hasslefreetraveldbcontext.Cabs.FindAsync(id);

            if (bus == null)
            {
                return NotFound();
            }

            return Ok(bus);
        }

        // POST: api/Cabs
        // POST: api/Cabs
        [HttpPost]
        public async Task<ActionResult<Cabs>> PostCab(CabDTO cabDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map CabDTO to Cabs entity
            var cabEntity = new Cabs
            {
                Cab_Model = cabDto.Cab_Model,
                Pickup_Location = cabDto.Pickup_Location,
                Dropoff_Location = cabDto.Dropoff_Location,
                Pickup_Time = cabDto.Pickup_Time,
                Dropoff_Time = cabDto.Dropoff_Time,
                FareAmount = cabDto.FareAmount,
                Distance = cabDto.Distance,
                CabType = cabDto.CabType
            };

            hasslefreetraveldbcontext.Cabs.Add(cabEntity);
            await hasslefreetraveldbcontext.SaveChangesAsync();

            // Return 201 Created response with the created entity
            return CreatedAtAction(nameof(GetCab), new { id = cabEntity.Cab_ID }, cabEntity);
        }
        // PUT: api/Cabs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCab(int id, CabDTO cabDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cabEntity = await hasslefreetraveldbcontext.Cabs.FindAsync(id);

            if (cabEntity == null)
            {
                return NotFound();
            }

            // Update the properties of cabEntity with values from cabDto
            cabEntity.Cab_Model = cabDto.Cab_Model;
            cabEntity.Pickup_Location = cabDto.Pickup_Location;
            cabEntity.Dropoff_Location = cabDto.Dropoff_Location;
            cabEntity.Pickup_Time = cabDto.Pickup_Time;
            cabEntity.Dropoff_Time = cabDto.Dropoff_Time;
            cabEntity.FareAmount = cabDto.FareAmount;
            cabEntity.Distance = cabDto.Distance;
            cabEntity.CabType = cabDto.CabType;

            hasslefreetraveldbcontext.Entry(cabEntity).State = EntityState.Modified;

            try
            {
                await hasslefreetraveldbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CabExists(id))
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


        // DELETE: api/Cabs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cabs>> DeleteCab(int id)
        {
            var cab = await hasslefreetraveldbcontext.Cabs.FindAsync(id);
            if (cab == null)
            {
                return NotFound();
            }

            hasslefreetraveldbcontext.Cabs.Remove(cab);
            await hasslefreetraveldbcontext.SaveChangesAsync();

            return cab;
        }

        private bool CabExists(int id)
        {
            return hasslefreetraveldbcontext.Cabs.Any(e => e.Cab_ID == id);
        }
    }
}
