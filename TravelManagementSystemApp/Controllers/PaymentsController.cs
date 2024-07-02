using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using TravelManagementSystemApp.Data;
using TravelManagementSystemApp.Models.Entities;

namespace TravelManagementSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext hasslefreetraveldbcontext;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(Hasslefreetraveldbcontext hasslefreetraveldbcontext, ILogger<PaymentsController> _logger)
        {
            this.hasslefreetraveldbcontext = hasslefreetraveldbcontext;
            this._logger = _logger;
        }
        // GET: api/payments
        /// <summary>
        /// Retrieves a list of all payments.
        /// </summary>
        /// <returns>A list of Payment objects</returns>
        /// <response code="200">Returns the list of payments</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Payments>>> GetPayments()
        {
            var payments = await hasslefreetraveldbcontext.Payments
                .Include(p => p.Bookings) // Include related Booking entity if needed
                .Select(p => new Payments
                {
                    Payment_ID = p.Payment_ID,
                    Booking_ID = p.Booking_ID,
                    Amount = p.Amount,
                    Payment_Date = p.Payment_Date,
                    Payment_Method = p.Payment_Method
                })
                .ToListAsync();

            return Ok(payments);
        }
        // GET: api/payments/{id}
        /// <summary>
        /// Retrieves a specific payment by ID.
        /// </summary>
        /// <param name="id">The ID of the payment to retrieve</param>
        /// <returns>A single Payment object</returns>
        /// <response code="200">Returns the payment with the specified ID</response>
        /// <response code="404">If no payment with the specified ID exists</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Payments>> GetPaymentById(int id)
        {
            var payment = await hasslefreetraveldbcontext.Payments
                .Include(p => p.Bookings) // Include related Booking entity if needed
                .FirstOrDefaultAsync(p => p.Payment_ID == id);

            if (payment == null)
            {
                return NotFound();
            }

            var paymentDto = new Payments
            {
                
                Booking_ID = payment.Booking_ID,
                Amount = payment.Amount,
                Payment_Date = payment.Payment_Date,
                Payment_Method = payment.Payment_Method
                // Add more properties as needed
            };

            return Ok(paymentDto);
        }

        // POST: api/payments
        /// <summary>
        /// Creates a new payment entry.
        /// </summary>
        /// <param name="payment">The Payment object containing the details to add</param>
        /// <returns>A newly created Payment object</returns>
        /// <response code="201">Returns the newly created payment</response>
        /// <response code="400">If the request body is null or invalid</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Payments>> PostPayment(PaymentDTO createPaymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Create a new Payments entity
                var payment = new Payments
                {
                    Booking_ID = createPaymentDto.Booking_ID,
                    Amount = createPaymentDto.Amount,
                    Payment_Date = createPaymentDto.Payment_Date,
                    Payment_Method = createPaymentDto.Payment_Method
                    // Add more properties as needed
                };

                hasslefreetraveldbcontext.Payments.Add(payment);
                await hasslefreetraveldbcontext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Payment_ID }, payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving payment");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the payment");
            }
        }

        

        // Add more controller methods as needed



        // PUT: api/payments/{id}
        /// <summary>
        /// Updates an existing payment record identified by ID.
        /// </summary>
        /// <param name="id">The ID of the payment to update</param>
        /// <param name="payment">The updated Payment object</param>
        /// <returns>The updated Payment object</returns>
        /// <response code="200">Returns the updated payment</response>
        /// <response code="400">If the request body or ID is invalid</response>
        /// <response code="404">If no payment with the specified ID exists</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPayment(int id, Payments updatePaymentDto)
        {
            

            var payment = await hasslefreetraveldbcontext.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            
            payment.Amount = updatePaymentDto.Amount;
            payment.Payment_Date = updatePaymentDto.Payment_Date;
            payment.Payment_Method = updatePaymentDto.Payment_Method;
            // Update other properties as needed

            hasslefreetraveldbcontext.Entry(payment).State = EntityState.Modified;

            try
            {
                await hasslefreetraveldbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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
        // DELETE: api/payments/{id}
        /// <summary>
        /// Deletes a payment record identified by ID.
        /// </summary>
        /// <param name="id">The ID of the payment to delete</param>
        /// <returns>No content if successful, NotFound if the payment with the specified ID does not exist</returns>
        /// <response code="204">No content if the payment is successfully deleted</response>
        /// <response code="404">If no payment with the specified ID exists</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await hasslefreetraveldbcontext.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            hasslefreetraveldbcontext.Payments.Remove(payment);
            await hasslefreetraveldbcontext.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return hasslefreetraveldbcontext.Payments.Any(e => e.Payment_ID == id);
        }
    }
}
