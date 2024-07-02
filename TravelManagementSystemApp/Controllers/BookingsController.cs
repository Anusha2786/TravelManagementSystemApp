using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Data;
using TravelManagementSystemApp.Models.Entities;

namespace TravelManagementSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext hasslefreetraveldbcontext;
        public BookingsController(Hasslefreetraveldbcontext hasslefreetraveldbcontext)
        {
            this.hasslefreetraveldbcontext = hasslefreetraveldbcontext;
        }
        // GET: api/bookings
        /// <summary>
        /// Retrieves a list of all Bookings.
        /// </summary>
        /// <returns>A list of Booking objects</returns>
        /// <response code="200">Returns the list of bookings</response>
        /// <response code="400">If the request is invalid or null</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
      
        public async Task<ActionResult<IEnumerable<BookingsDTO>>> GetBookings()
        {
            var bookings = await hasslefreetraveldbcontext.Bookings
                .Include(b => b.User) // Include related User entity if needed
                .Select(b => new BookingsDTO
                {
                    
                    Booking_Date = b.Booking_Date,
                    Booking_Type = b.Booking_Type,
                    Booking_Status = b.Booking_Status,
                    User = new UserDTO
                    {
                        Firstname = b.User.Firstname,
                        Lastname = b.User.Lastname,
                        Email = b.User.Email,
                    }
                })
                .ToListAsync();

            return Ok(bookings);
        }
        // GET: api/bookings/{id}
        /// <summary>
        /// Retrieves a specific Booking by ID.
        /// </summary>
        /// <param name="id">The ID of the Booking to retrieve</param>
        /// <returns>A single Booking object</returns>
        /// <response code="200">Returns the booking with the specified ID</response>
        /// <response code="400">If the request is invalid or null</response>
        /// <response code="404">If no booking with the specified ID exists</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingsDTO>> GetBookingById(int id)
        {
            var booking = await hasslefreetraveldbcontext.Bookings
                .Include(b => b.User) // Include related User entity if needed
                .FirstOrDefaultAsync(b => b.Booking_ID == id);

            if (booking == null)
            {
                return NotFound();
            }

            var bookingDto = new BookingsDTO
            {
              
                Booking_Date = booking.Booking_Date,
                Booking_Type = booking.Booking_Type,
                Booking_Status = booking.Booking_Status,
                User = new UserDTO
                {
                    Firstname = booking.User.Firstname,
                    Lastname = booking.User.Lastname,
                    Email = booking.User.Email,
                }
            };

            return Ok(bookingDto);

        }

        // POST: api/bookings
        /// <summary>
        /// Creates a new Booking entry.
        /// </summary>
        /// <param name="booking">The Booking object containing the details to add</param>
        /// <returns>A newly created Booking object</returns>
        /// <response code="201">Returns the newly created booking</response>
        /// <response code="400">If the request body is null or invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookingsDTO>> PostBooking(BookingsDTO createBookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the provided user exists
            var user = await hasslefreetraveldbcontext.users.FindAsync(createBookingDto.User.userid);
            if (user == null)
            {
                return BadRequest("User with the provided userid does not exist.");
            }

            var booking = new Bookings
            {
                userid = createBookingDto.User.userid, // Ensure userid corresponds to an existing user
                Booking_Date = createBookingDto.Booking_Date,
                Booking_Type = createBookingDto.Booking_Type,
                Booking_Status = createBookingDto.Booking_Status,
                // Initialize other properties as needed
            };

            hasslefreetraveldbcontext.Bookings.Add(booking);
            await hasslefreetraveldbcontext.SaveChangesAsync();

            var bookingDto = new BookingsDTO
            {
                Booking_Date = booking.Booking_Date,
                Booking_Type = booking.Booking_Type,
                Booking_Status = booking.Booking_Status,
                User = new UserDTO
                {
                    userid = booking.userid,
                    // Map other user properties if needed
                }
            };

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Booking_ID }, bookingDto);
        }

        // PUT: api/bookings/{id}
        /// <summary>
        /// Updates an existing Booking record identified by ID.
        /// </summary>
        /// <param name="id">The ID of the Booking to update</param>
        /// <param name="booking">The updated Booking object</param>
        /// <returns>The updated Booking object</returns>
        /// <response code="200">Returns the updated booking</response>
        /// <response code="400">If the request body or ID is invalid</response>
        /// <response code="404">If no booking with the specified ID exists</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutBooking(int id, BookingsDTO updateBookingDto)
        {
            

            var booking = await hasslefreetraveldbcontext.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound("Booking not found");
            }

            // Update fields based on DTO
            booking.Booking_Date = updateBookingDto.Booking_Date;
            booking.Booking_Type = updateBookingDto.Booking_Type;
            booking.Booking_Status = updateBookingDto.Booking_Status;
            // Update other properties as needed

            try
            {
                await hasslefreetraveldbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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
        // DELETE: api/bookings/{id}
        /// <summary>
        /// Deletes a Booking record identified by ID.
        /// </summary>
        /// <param name="id">The ID of the booking to delete</param>
        /// <returns>No content if successful, NotFound if the booking with the specified ID does not exist</returns>
        /// <response code="204">No content if the booking is successfully deleted</response>
        /// <response code="404">If no booking with the specified ID exists</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await hasslefreetraveldbcontext.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound("Booking not found");
            }

            hasslefreetraveldbcontext.Bookings.Remove(booking);
            await hasslefreetraveldbcontext.SaveChangesAsync();

            return NoContent();
        }
        private bool BookingExists(int id)
        {
            return hasslefreetraveldbcontext.Bookings.Any(e => e.Booking_ID == id);
        }



    }
}
