﻿using System;
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
        private readonly TravelManagementSystemAppContext _context;

        public ReviewsController(TravelManagementSystemAppContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
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

        // GET: api/Reviews/5
        [HttpGet("{id}")]
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

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reviews>> PostReviews(Reviews reviews)
        {
            _context.Reviews.Add(reviews);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReviews", new { id = reviews.ReviewID }, reviews);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
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
