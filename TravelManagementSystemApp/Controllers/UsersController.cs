using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelManagementSystemApp.Data;
using TravelManagementSystemApp.Models;

namespace TravelManagementSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersDbContext context;
        private readonly ILogger<UsersController> logger;

        public UsersController(UsersDbContext context, ILogger<UsersController> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await context.users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user = await context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<Users>> PostUser(Users user)
        {
            try
            {
                context.users.Add(user);
                await context.SaveChangesAsync();
                return CreatedAtAction("GetUser", new { id = user.userid }, user);
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "Error adding user to database");
                return StatusCode(500, "Error adding user to database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding user");
                return StatusCode(500, "Error adding user");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, Users user)
        {
            if (id != user.userid)
            {
                return BadRequest();
            }

            try
            {
                context.Entry(user).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.LogError(ex, "Error updating user in database");
                return StatusCode(500, "Error updating user in database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating user");
                return StatusCode(500, "Error updating user");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUser(int id)
        {
            var user = await context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            try
            {
                context.users.Remove(user);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "Error deleting user from database");
                return StatusCode(500, "Error deleting user from database");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting user");
                return StatusCode(500, "Error deleting user");
            }
        }
    }
}