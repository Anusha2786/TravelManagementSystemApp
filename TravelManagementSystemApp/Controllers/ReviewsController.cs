using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Data;
using TravelManagementSystemApp.Models;

namespace TravelManagementSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext _context;

        public ReviewsController(Hasslefreetraveldbcontext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all reviews.
        /// </summary>
        /// <returns>A list of Review objects</returns>
        /// <response code="200">Returns the list of reviews</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP 200 - OK
        public async Task<ActionResult<IEnumerable<Reviews>>> GetReviews([FromQuery] int? userID)
        {
            if (userID.HasValue)
            {
                return await _context.Reviews.Where(r => r.UserID == userID.Value).ToListAsync();
            }
            else
            {
                return await _context.Reviews.ToListAsync();
            }
        }

        /// <summary>
        /// Retrieves a specific review by ID.
        /// </summary>
        /// <param name="id">The ID of the review to retrieve</param>
        /// <returns>The review with the specified ID</returns>
        /// <response code="200">Returns the review</response>
        /// <response code="404">If a review with the specified ID is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // HTTP 200 - OK
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<ActionResult<Reviews>> GetReviews(int id)
        {
            var reviews = await _context.Reviews.FindAsync(id);

            if (reviews == null)
            {
                return NotFound();
            }

            return reviews;
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReviews(int id, Reviews reviews)
        {
            if (id != reviews.ReviewID)
            {
                return BadRequest();
            }

            _context.Entry(reviews).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewsExists(id))
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

        /// <summary>
        /// Creates a new review.
        /// </summary>
        /// <param name="review">The review data to create</param>
        /// <returns>A newly created review</returns>
        /// <response code="201">Returns the newly created review</response>
        /// <response code="400">If the review data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] // HTTP 201 - Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // HTTP 400 - Bad Request
        public async Task<ActionResult<Reviews>> PostReviews(Reviews reviews)
        {
            _context.Reviews.Add(reviews);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReviews", new { id = reviews.ReviewID }, reviews);
        }

        /// <summary>
        /// Deletes a specific review by ID.
        /// </summary>
        /// <param name="id">The ID of the review to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">Returns when the review is successfully deleted</response>
        /// <response code="404">If no review with the specified ID exists</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // HTTP 204 - No Content
        [ProducesResponseType(StatusCodes.Status404NotFound)] // HTTP 404 - Not Found
        public async Task<IActionResult> DeleteReviews(int id)
        {
            var reviews = await _context.Reviews.FindAsync(id);
            if (reviews == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(reviews);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewsExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewID == id);
        }
    }
}
