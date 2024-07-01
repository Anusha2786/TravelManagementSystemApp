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
    public class TrainsController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext hasslefreetraveldbcontext;
        public TrainsController(Hasslefreetraveldbcontext hasslefreetraveldbcontext)
        {
            this.hasslefreetraveldbcontext = hasslefreetraveldbcontext;
        }
        // GET: api/Trains
        /// <summary>
        /// Retrieves a list of all trains.
        /// </summary>
        /// <returns>A list of Train objects</returns>
        /// <response code="200">Returns the list of trains</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TrainDTO>>> GetCabs()
        {
            var cabs = await hasslefreetraveldbcontext.Trains.ToListAsync();
            return Ok(cabs);
        }

        // GET: api/Trains/{id}
        /// <summary>
        /// Retrieves a specific train by ID.
        /// </summary>
        /// <param name="id">The ID of the train to retrieve</param>
        /// <returns>A single Train object</returns>
        /// <response code="200">Returns the train with the specified ID</response>
        /// <response code="404">If no train with the specified ID exists</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TrainDTO>> GetTrain(int id)
        {
            var Train = await hasslefreetraveldbcontext.Trains.FindAsync(id);

            if (Train == null)
            {
                return NotFound();
            }

            return Ok(Train);
        }
        // POST: api/Trains
        /// <summary>
        /// Creates a new train entry.
        /// </summary>
        /// <param name="train">The Train object containing the details to add</param>
        /// <returns>A newly created Train object</returns>
        /// <response code="201">Returns the newly created train</response>
        /// <response code="400">If the request body is null or invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Trains>> PostTrain(TrainDTO trainDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var train = new Trains
            {
                Train_Number = trainDto.Train_Number,
                Train_Name = trainDto.Train_Name,
                Departure_Station = trainDto.Departure_Station,
                Arrival_Station = trainDto.Arrival_Station,
                Available_Seats = trainDto.AvailableSeats
            };

            hasslefreetraveldbcontext.Trains.Add(train);
            await hasslefreetraveldbcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTrain), new { id = train.Train_ID }, train);
        }
        // PUT: api/Trains/{id}
        /// <summary>
        /// Updates an existing train record identified by ID.
        /// </summary>
        /// <param name="id">The ID of the train to update</param>
        /// <param name="train">The updated Train object</param>
        /// <returns>The updated Train object</returns>
        /// <response code="200">Returns the updated train</response>
        /// <response code="400">If the request body or ID is invalid</response>
        /// <response code="404">If no train with the specified ID exists</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutTrain(int id, TrainDTO trainDto)
        {
            if (id != trainDto.Train_Number)
            {
                return BadRequest();
            }

            var train = await hasslefreetraveldbcontext.Trains.FindAsync(id);

            if (train == null)
            {
                return NotFound();
            }

            // Update properties with values from DTO
            train.Train_Number = trainDto.Train_Number;
            train.Train_Name = trainDto.Train_Name;
            train.Departure_Station = trainDto.Departure_Station;
            train.Arrival_Station = trainDto.Arrival_Station;
            train.Available_Seats = trainDto.AvailableSeats;

            try
            {
                await hasslefreetraveldbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainExists(id))
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

        private bool TrainExists(int id)
        {
            return hasslefreetraveldbcontext.Trains.Any(e => e.Train_ID == id);
        }

        // DELETE: api/Trains/{id}
        /// <summary>
        /// Deletes a train record identified by ID.
        /// </summary>
        /// <param name="id">The ID of the train to delete</param>
        /// <returns>No content if successful, NotFound if the train with the specified ID does not exist</returns>
        /// <response code="204">No content if the train is successfully deleted</response>
        /// <response code="404">If no train with the specified ID exists</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTrain(int id)
        {
            var train = await hasslefreetraveldbcontext.Trains.FindAsync(id);
            if (train == null)
            {
                return NotFound();
            }

            hasslefreetraveldbcontext.Trains.Remove(train);
            await hasslefreetraveldbcontext.SaveChangesAsync();

            return NoContent();
        }



    }
}

