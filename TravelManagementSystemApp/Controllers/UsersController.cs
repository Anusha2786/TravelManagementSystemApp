using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
       

    }
}
