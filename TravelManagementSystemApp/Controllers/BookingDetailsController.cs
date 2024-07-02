using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using TravelManagementSystemApp.Data;
using TravelManagementSystemApp.Models.Entities;

namespace TravelManagementSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingDetailsController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext hasslefreetraveldbcontext;
        //private readonly JsonSerializerOptions _jsonOptions;
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
        public class BookingDetailDTO
        {
            public int Booking_Detail_ID { get; set; }
            public int Booking_ID { get; set; }
            public string Detail_Type { get; set; }
            // Other properties as needed
        }

        /// <summary>
        /// Creates a new booking detail.
        /// </summary>
        /// <param name="bookingDetailsDTO">The details of the booking to create</param>
        /// <returns>A newly created BookingDetailDTO</returns>
        /// <response code="201">Returns the newly created booking detail</response>
        /// <response code="400">If the request is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookingDetailDTO>> PostBookingDetail(BookingDetailsDTO bookingDetailsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookingDetail = new Booking_Details
            {
                Booking_ID = bookingDetailsDTO.Booking_ID,
                Detail_Type = bookingDetailsDTO.Detail_Type,
                // Map other properties as needed
            };

            hasslefreetraveldbcontext.Booking_Details.Add(bookingDetail);
            await hasslefreetraveldbcontext.SaveChangesAsync();

            var bookingDetailDTO = new BookingDetailDTO
            {
                Booking_Detail_ID = bookingDetail.Booking_Detail_ID,
                Booking_ID = bookingDetail.Booking_ID,
                Detail_Type = bookingDetail.Detail_Type,
                // Map other properties as needed
            };

            return CreatedAtAction(nameof(GetBookingDetailById), new { id = bookingDetailDTO.Booking_Detail_ID }, bookingDetailDTO);
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
