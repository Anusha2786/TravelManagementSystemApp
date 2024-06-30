using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Data;
using TravelManagementSystemApp.Models.Entities;

namespace TravelManagementSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingDetailsController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext hasslefreetraveldbcontext;
        public BookingDetailsController(Hasslefreetraveldbcontext hasslefreetraveldbcontext)
        {
            this.hasslefreetraveldbcontext = hasslefreetraveldbcontext;
        }

        // GET: api/bookingdetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDetailsDTO>>> GetBookingDetails()
        {
            var bookingDetails = await hasslefreetraveldbcontext.Booking_Details
                .Include(bd => bd.Bookings) // Include related Booking entity
                .ToListAsync();

            return Ok(bookingDetails);
        }
        // GET: api/bookingdetails/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDetailsDTO>> GetBookingDetailById(int id)
        {
            var bookingDetail = await hasslefreetraveldbcontext.Booking_Details
                .Include(bd => bd.Bookings) // Include related Booking entity
                .FirstOrDefaultAsync(bd => bd.Booking_Detail_ID == id);

            if (bookingDetail == null)
            {
                return NotFound(); // Return 404 Not Found if booking detail with the given ID is not found
            }

            return Ok(bookingDetail);
        }
        // POST: api/bookingdetails
        [HttpPost]
        public async Task<ActionResult<Booking_Details>> PostBookingDetail(BookingDetailsDTO bookingDetailsDTO)
        {
            // Validate input data if necessary
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to entity model
            var bookingDetail = new Booking_Details
            {
                
                Detail_Type = bookingDetailsDTO.Detail_Type,
                
            };

            // Add booking detail to DbContext and save changes
            hasslefreetraveldbcontext.Booking_Details.Add(bookingDetail);
            await hasslefreetraveldbcontext.SaveChangesAsync();

            // Return created booking detail with 201 Created status
            return CreatedAtAction(nameof(GetBookingDetailById), new { id = bookingDetail.Booking_Detail_ID }, bookingDetail);
        }
        // PUT: api/bookingdetails/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingDetail(int id, BookingDetailsDTO bookingDetailsDTO)
        {
            

            // Find the existing booking detail in the database
            var bookingDetail = await hasslefreetraveldbcontext.Booking_Details.FindAsync(id);
            if (bookingDetail == null)
            {
                return NotFound(); // Return 404 Not Found if booking detail with the given ID is not found
            }

            // Update properties from DTO to entity
            
            bookingDetail.Detail_Type = bookingDetailsDTO.Detail_Type;
            

            // Save changes to the database
            try
            {
                await hasslefreetraveldbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exceptions if needed
                throw;
            }

            return NoContent(); // Return 204 No Content upon successful update
        }
        // DELETE: api/bookingdetails/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingDetail(int id)
        {
            var bookingDetail = await hasslefreetraveldbcontext.Booking_Details.FindAsync(id);
            if (bookingDetail == null)
            {
                return NotFound(); // Return 404 Not Found if booking detail with the given ID is not found
            }

            // Remove the booking detail from the context
            hasslefreetraveldbcontext.Booking_Details.Remove(bookingDetail);
            await hasslefreetraveldbcontext.SaveChangesAsync();

            return NoContent(); // Return 204 No Content upon successful deletion
        }



    }
}
