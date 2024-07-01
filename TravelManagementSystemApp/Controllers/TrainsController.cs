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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainDTO>>> GetCabs()
        {
            var cabs = await hasslefreetraveldbcontext.Trains.ToListAsync();
            return Ok(cabs);
        }

        [HttpGet("{id}")]
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
        [HttpPost]
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
        // PUT: api/Trains/5
        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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

