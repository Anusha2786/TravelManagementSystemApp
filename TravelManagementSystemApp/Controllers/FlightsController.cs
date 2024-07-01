using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Add this using directive
using TravelManagementSystemApp.Data;
using TravelManagementSystemApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelManagementSystemApp.Models.Entities;

namespace TravelManagementSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FlightsController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext hasslefreetraveldbcontext;
        public FlightsController(Hasslefreetraveldbcontext hasslefreetraveldbcontext)
        {
            this.hasslefreetraveldbcontext = hasslefreetraveldbcontext;
        }
        // GET: api/flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightsDTO>>> GetCabs()
        {
            var cabs = await hasslefreetraveldbcontext.Flights.ToListAsync();
            return Ok(cabs);
        }

        // GET: api/flights/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightsDTO>> GetFlightById(int id)
        {
            var flight = await hasslefreetraveldbcontext.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            var flightDTO = new FlightsDTO
            {
                Flight_Number = flight.Flight_Number,
                Airline = flight.Airline,
                Departure_Airport = flight.Departure_Airport,
                Arrival_Airport = flight.Arrival_Airport,
                Total_Seats = flight.Total_Seats,
                Available_Seats = flight.Available_Seats
            };

            return Ok(flightDTO);
        }

        // POST: api/flights

        // POST: api/flights
        [HttpPost]
        public ActionResult<Flights> PostFlight(FlightsDTO flightsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flight = new Flights
            {
                Flight_Number = flightsDTO.Flight_Number,
                Airline = flightsDTO.Airline,
                Departure_Airport = flightsDTO.Departure_Airport,
                Arrival_Airport = flightsDTO.Arrival_Airport,
                Total_Seats = flightsDTO.Total_Seats,
                Available_Seats = flightsDTO.Available_Seats
            };

            hasslefreetraveldbcontext.Flights.Add(flight);
            hasslefreetraveldbcontext.SaveChanges();

            // Return 201 Created response with the newly created flight
            return CreatedAtAction(nameof(GetFlightById), new { id = flight.Flight_ID }, flight);
        }
        // PUT: api/flights/{id}
        [HttpPut("{id}")]
        public IActionResult PutFlight(int id, FlightsDTO flightsDTO)
        {
           
            var flight = hasslefreetraveldbcontext.Flights.Find(id);
            if (flight == null)
            {
                return NotFound();
            }

            // Update flight properties
            flight.Flight_Number = flightsDTO.Flight_Number;
            flight.Airline = flightsDTO.Airline;
            flight.Departure_Airport = flightsDTO.Departure_Airport;
            flight.Arrival_Airport = flightsDTO.Arrival_Airport;
            flight.Total_Seats = flightsDTO.Total_Seats;
            flight.Available_Seats = flightsDTO.Available_Seats;

            try
            {
                hasslefreetraveldbcontext.Entry(flight).State = EntityState.Modified;
                hasslefreetraveldbcontext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
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
        // DELETE: api/flights/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteFlight(int id)
        {
            var flight = hasslefreetraveldbcontext.Flights.Find(id);
            if (flight == null)
            {
                return NotFound();
            }

            hasslefreetraveldbcontext.Flights.Remove(flight);
            hasslefreetraveldbcontext.SaveChanges();

            return NoContent();
        }
        private bool FlightExists(int id)
        {
            return hasslefreetraveldbcontext.Flights.Any(e => e.Flight_ID == id);
        }

    }
}