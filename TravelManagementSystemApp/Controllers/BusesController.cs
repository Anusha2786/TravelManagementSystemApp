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
        /// <summary>
        /// Retrieves a list of all buses.
        /// </summary>
        /// <returns>A list of Bus objects</returns>
        /// <response code="200">Returns the list of buses</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BusDTO>>> GetBuses()
        {
            var buses = await hasslefreetraveldbcontext.Buses.ToListAsync();
            return Ok(buses);


        }
        /// <summary>
        /// Retrieves a specific bus by ID.
        /// </summary>
        /// <param name="id">The ID of the bus to retrieve</param>
        /// <returns>A single Bus object</returns>
        /// <response code="200">Returns the bus with the specified ID</response>
        /// <response code="404">If no bus with the specified ID exists</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BusDTO>> GetBusById(int id)
        {
            var bus = await hasslefreetraveldbcontext.Buses.FindAsync(id);

            if (bus == null)
            {
                return NotFound();
            }

            return Ok(bus);
        }

        // / POST: api/Buses
        /// <summary>
        /// Creates a new bus entry.
        /// </summary>
        /// <param name="bus">The Bus object containing the details to add</param>
        /// <returns>A newly created Bus object</returns>
        /// <response code="201">Returns the newly created bus</response>
        /// <response code="400">If the request body is null or invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

            // Add new bus entity to the database context
            hasslefreetraveldbcontext.Buses.Add(busEntity);

            // Save changes asynchronously
            await hasslefreetraveldbcontext.SaveChangesAsync();

            // Return 201 Created response with the created entity
            return CreatedAtAction(nameof(GetBusById), new { id = busEntity.Bus_ID }, busEntity);
        }



        // PUT: api/Buses/5
        /// <summary>
        /// Updates an existing bus record identified by ID.
        /// </summary>
        /// <param name="id">The ID of the bus to update</param>
        /// <param name="bus">The updated Bus object</param>
        /// <returns>The updated Bus object</returns>
        /// <response code="200">Returns the updated bus</response>
        /// <response code="400">If the request body or ID is invalid</response>
        /// <response code="404">If no bus with the specified ID exists</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Deletes a bus record identified by ID.
        /// </summary>
        /// <param name="id">The ID of the bus to delete</param>
        /// <returns>No content if successful, NotFound if the bus with the specified ID does not exist</returns>
        /// <response code="204">No content if the bus is successfully deleted</response>
        /// <response code="404">If no bus with the specified ID exists</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
