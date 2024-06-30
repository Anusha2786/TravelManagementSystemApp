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
        public async Task<ActionResult<IEnumerable<FlightsDTO>>> GetFlights()
        {
            var flights = await hasslefreetraveldbcontext.Flights
                .Select(f => new FlightsDTO
                {
                    Flight_ID = f.Flight_ID,
                    Flight_Number = f.Flight_Number,
                    Airline = f.Airline,
                    Departure_Airport = f.Departure_Airport,
                    Arrival_Airport = f.Arrival_Airport,
                    Total_Seats = f.Total_Seats,
                    Available_Seats = f.Available_Seats
                }).ToListAsync();

            return Ok(flights);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightsDTO>> GetFlightById(int id)
        {
            // Find the flight with the given ID in the database
            var flight = await hasslefreetraveldbcontext.Flights
                .FirstOrDefaultAsync(f => f.Flight_ID == id);

            // If flight is not found, return 404 Not Found
            if (flight == null)
            {
                return NotFound();
            }

            // Map the Flight entity to FlightsDTO
            var flightDTO = new FlightsDTO
            {
                Flight_ID = flight.Flight_ID,
                Flight_Number = flight.Flight_Number,
                Airline = flight.Airline,
                Departure_Airport = flight.Departure_Airport,
                Arrival_Airport = flight.Arrival_Airport,
                Total_Seats = flight.Total_Seats,
                Available_Seats = flight.Available_Seats
            };

            // Return the flight DTO with 200 OK status
            return Ok(flightDTO);
        }
        [HttpPost]
        public async Task<ActionResult<FlightsDTO>> PostFlight(FlightsDTO flightDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to entity
            var flight = new Flights
            {
                Flight_Number = flightDto.Flight_Number,
                Airline = flightDto.Airline,
                Departure_Airport = flightDto.Departure_Airport,
                Arrival_Airport = flightDto.Arrival_Airport,
                Total_Seats = flightDto.Total_Seats,
                Available_Seats = flightDto.Available_Seats
            };

            // Add to database and save changes
            hasslefreetraveldbcontext.Flights.Add(flight);
            await hasslefreetraveldbcontext.SaveChangesAsync();

            // Return the created flight DTO with 201 Created status
            // You can optionally include a link to get the details of the newly created resource
            var flightDtoResponse = new FlightsDTO
            {
               
                Flight_Number = flight.Flight_Number,
                Airline = flight.Airline,
                Departure_Airport = flight.Departure_Airport,
                Arrival_Airport = flight.Arrival_Airport,
                Total_Seats = flight.Total_Seats,
                Available_Seats = flight.Available_Seats
            };

            return CreatedAtAction(nameof(GetFlightById), new { id = flight.Flight_ID }, flightDtoResponse);
        }
        // PUT: api/flights/5
        // PUT: api/flights/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(int id, PutFlightsDTO updateFlightDto)
        {


            var flight = await hasslefreetraveldbcontext.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            // Update flight entity with data from DTO
            flight.Flight_Number = updateFlightDto.Flight_Number;
            flight.Airline = updateFlightDto.Airline;
            flight.Departure_Airport = updateFlightDto.Departure_Airport;
            flight.Arrival_Airport = updateFlightDto.Arrival_Airport;
            flight.Total_Seats = updateFlightDto.Total_Seats;
            flight.Available_Seats = updateFlightDto.Available_Seats;

            try
            {
                await hasslefreetraveldbcontext.SaveChangesAsync();
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var flight = await hasslefreetraveldbcontext.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            hasslefreetraveldbcontext.Flights.Remove(flight);
            await hasslefreetraveldbcontext.SaveChangesAsync();

            return NoContent();
        }


        private bool FlightExists(int id)
        {
            return hasslefreetraveldbcontext.Flights.Any(e => e.Flight_ID == id);
        }

    }
}

    
