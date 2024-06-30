using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Data;
using TravelManagementSystemApp.Models.Entities;

namespace TravelManagementSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly Hasslefreetraveldbcontext hasslefreetraveldbcontext;
        public PaymentsController(Hasslefreetraveldbcontext hasslefreetraveldbcontext)
        {
            this.hasslefreetraveldbcontext = hasslefreetraveldbcontext;
        }
        // GET: api/payments
        [HttpGet]
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
        // GET: api/payments/5
        [HttpGet("{id}")]
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
        [HttpPost]
        public async Task<ActionResult<Payments>> PostPayment(Payments createPaymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payment = new Payments
            {
                
                Amount = createPaymentDto.Amount,
                Payment_Date = createPaymentDto.Payment_Date,
                Payment_Method = createPaymentDto.Payment_Method
                // Add more properties as needed
            };

            hasslefreetraveldbcontext.Payments.Add(payment);
            await hasslefreetraveldbcontext.SaveChangesAsync();

            var paymentDto = new Payments
            {
               
                Amount = payment.Amount,
                Payment_Date = payment.Payment_Date,
                Payment_Method = payment.Payment_Method
                // Map other properties if needed
            };

            return CreatedAtAction(nameof(GetPaymentById), new { id = paymentDto.Payment_ID }, paymentDto);
        }

        // PUT: api/payments/5
        [HttpPut("{id}")]
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
        // DELETE: api/payments/5
        [HttpDelete("{id}")]
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
