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
        [HttpGet]
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
        [HttpGet("{id}")]
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
        [HttpPost]
        public async Task<ActionResult<BookingsDTO>> PostBooking(BookingsDTO createBookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var booking = new Bookings
            {
              
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
                // Map other properties if needed
            };

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Booking_ID }, bookingDto);
        }
        // PUT: api/bookings/{id}
        [HttpPut("{id}")]
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
        [HttpDelete("{id}")]
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
