using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Data;
using TravelManagementSystemApp.Models.Entities;

namespace TravelManagementSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext hasslefreetraveldbcontext;
        public BusesController(Hasslefreetraveldbcontext hasslefreetraveldbcontext)
        {
            this.hasslefreetraveldbcontext = hasslefreetraveldbcontext;
        }
        // GET: api/Buses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BusDTO>>> GetBuses()
        {
            var buses = await hasslefreetraveldbcontext.Buses.ToListAsync();
            return Ok(buses);


        }
        // GET: api/Buses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BusDTO>> GetBusById(int id)
        {
            var bus = await hasslefreetraveldbcontext.Buses.FindAsync(id);

            if (bus == null)
            {
                return NotFound();
            }

            return Ok(bus);
        }

        [HttpPost]
        public async Task<ActionResult<Buses>> PostBus(BusDTO busDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map BusDTO to Buses entity
            var busEntity = new Buses
            {
                Bus_Number = busDto.Bus_Number,
                Bus_Name = busDto.Bus_Name,
                From_Location = busDto.From_Location,
                To_Location = busDto.To_Location,
                Total_Seats = busDto.Total_Seats,
                Available_Seats = busDto.Available_Seats
            };

            hasslefreetraveldbcontext.Buses.Add(busEntity);
            await hasslefreetraveldbcontext.SaveChangesAsync();

            // Return 201 Created response with the created entity
            return CreatedAtAction(nameof(GetBusById), new { id = busEntity.Bus_ID }, busEntity);
        }


        // PUT: api/Buses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBus(int id, BusDTO busDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bus = await hasslefreetraveldbcontext.Buses.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }

            bus.Bus_Number = busDto.Bus_Number;
            bus.Bus_Name = busDto.Bus_Name;
            bus.From_Location = busDto.From_Location;
            bus.To_Location = busDto.To_Location;
            bus.Total_Seats = busDto.Total_Seats;
            bus.Available_Seats = busDto.Available_Seats;

            hasslefreetraveldbcontext.Entry(bus).State = EntityState.Modified;

            try
            {
                await hasslefreetraveldbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusExists(id))
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
        // DELETE: api/Buses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBus(int id)
        {
            var bus = await hasslefreetraveldbcontext.Buses.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }

            hasslefreetraveldbcontext.Buses.Remove(bus);
            await hasslefreetraveldbcontext.SaveChangesAsync();

            return NoContent();
        }

        private bool BusExists(int id)
        {
            return hasslefreetraveldbcontext.Buses.Any(e => e.Bus_ID == id);
        }
    }
}
