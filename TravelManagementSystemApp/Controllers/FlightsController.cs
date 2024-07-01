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
        /// <summary>
        /// Retrieves a list of all flights.
        /// </summary>
        /// <returns>A list of Flight objects</returns>
        /// <response code="200">Returns the list of flights</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FlightsDTO>>> GetCabs()
        {
            var cabs = await hasslefreetraveldbcontext.Flights.ToListAsync();
            return Ok(cabs);
        }

        // GET: api/flights/{id}
        /// <summary>
        /// Retrieves a specific flight by ID.
        /// </summary>
        /// <param name="id">The ID of the flight to retrieve</param>
        /// <returns>A single Flight object</returns>
        /// <response code="200">Returns the flight with the specified ID</response>
        /// <response code="404">If no flight with the specified ID exists</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Creates a new flight entry.
        /// </summary>
        /// <param name="flight">The Flight object containing the details to add</param>
        /// <returns>A newly created Flight object</returns>
        /// <response code="201">Returns the newly created flight</response>
        /// <response code="400">If the request body is null or invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <summary>
        /// Updates an existing flight record identified by ID.
        /// </summary>
        /// <param name="id">The ID of the flight to update</param>
        /// <param name="flight">The updated Flight object</param>
        /// <returns>The updated Flight object</returns>
        /// <response code="200">Returns the updated flight</response>
        /// <response code="400">If the request body or ID is invalid</response>
        /// <response code="404">If no flight with the specified ID exists</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Deletes a flight record identified by ID.
        /// </summary>
        /// <param name="id">The ID of the flight to delete</param>
        /// <returns>No content if successful, NotFound if the flight with the specified ID does not exist</returns>
        /// <response code="204">No content if the flight is successfully deleted</response>
        /// <response code="404">If no flight with the specified ID exists</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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