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
        /// <summary>
        /// Retrieves a list of all Booking Details.
        /// </summary>
        /// <returns>A list of BookingDetailsDTO objects</returns>
        /// <response code="200">Returns the list of booking details</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BookingDetailsDTO>>> GetBookingDetails()
        {
            var bookingDetails = await hasslefreetraveldbcontext.Booking_Details
                .Include(bd => bd.Bookings) // Include related Booking entity
                .ToListAsync();

            return Ok(bookingDetails);
        }
        /// <summary>
        /// Retrieves a specific Booking Details by ID.
        /// </summary>
        /// <param name="id">The ID of the Booking Details to retrieve</param>
        /// <returns>A specific BookingDetailsDTO object</returns>
        /// <response code="200">Returns the booking details with the specified ID</response>
        /// <response code="404">If no booking details with the specified ID exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Creates a new Booking Details entry.
        /// </summary>
        
        
        /// <response code="201">Returns the newly created booking details</response>
        /// <response code="400">If the request body is null or invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// PUT: api/bookingdetails/{id}
        /// <summary>
        /// Updates the Booking Details identified by ID.
        /// </summary>
        /// <param name="id">The ID of the Booking Details to update</param>
        /// <param name="bookingDetailsDTO">The updated BookingDetailsDTO object</param>
       
        /// <response code="200">Returns the updated booking details</response>
        /// <response code="400">If the request body or ID is invalid</response>
        /// <response code="404">If no booking details with the specified ID exist</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Deletes the Booking Details identified by ID.
        /// </summary>
        /// <param name="id">The ID of the Booking Details to delete</param>
        /// <returns>No content if successful, NotFound if the booking details with the specified ID do not exist</returns>
        /// <response code="204">No content if the booking details are successfully deleted</response>
        /// <response code="404">If no booking details with the specified ID exist</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
