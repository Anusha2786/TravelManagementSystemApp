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
    public class AddresesController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext _context;

        public AddresesController(Hasslefreetraveldbcontext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all addresses.
        /// </summary>
        /// <returns>A list of all addresses</returns>
        /// <response code="200">Returns the list of addresses</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Addreses>>> GetAddreses()
        {
            return await _context.Addreses.ToListAsync();
        }

        /// <summary>
        /// Retrieves an address by ID.
        /// </summary>
        /// <param name="id">The ID of the address to retrieve</param>
        /// <returns>The address object with the specified ID</returns>
        /// <response code="200">Returns the address with the specified ID</response>
        /// <response code="404">If no address with the specified ID exists</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Addreses>> GetAddreses(int id)
        {
            var addreses = await _context.Addreses.FindAsync(id);

            if (addreses == null)
            {
                return NotFound();
            }

            return addreses;
        }

        /// <summary>
        /// Updates an existing address.
        /// </summary>
        /// <param name="id">The ID of the address to update</param>
        /// <param name="address">The updated address object</param>
        /// <returns>No content</returns>
        /// <response code="204">Indicates the address was successfully updated</response>
        /// <response code="400">If the ID does not match the address ID or the model state is invalid</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAddreses(int id, Addreses addreses)
        {
            if (id != addreses.AddressID)
            {
                return BadRequest();
            }

            _context.Entry(addreses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddresesExists(id))
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
        /// Creates a new address.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Addresses
        ///     {
        ///         "name": "New Address Name",
        ///         "code": "NEW",
        ///         "city": "New City",
        ///         "state": "New State",
        ///         "country": "New Country"
        ///     }
        ///
        /// </remarks>
        /// <param name="address">The address object to create</param>
        /// <returns>A newly created address</returns>
        /// <response code="201">Returns the newly created address</response>
        /// <response code="400">If the model state is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Addreses>> PostAddreses(Addreses addreses)
        {
            _context.Addreses.Add(addreses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddreses", new { id = addreses.AddressID }, addreses);
        }

        /// <summary>
        /// Deletes a specific address by ID.
        /// </summary>
        /// <param name="id">The ID of the address to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">Returns when the address is successfully deleted</response>
        /// <response code="404">If no address with the specified ID exists</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAddreses(int id)
        {
            var addreses = await _context.Addreses.FindAsync(id);
            if (addreses == null)
            {
                return NotFound();
            }

            _context.Addreses.Remove(addreses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddresesExists(int id)
        {
            return _context.Addreses.Any(e => e.AddressID == id);
        }
    }
}
