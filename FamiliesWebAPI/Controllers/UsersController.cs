using System;
using System.Threading.Tasks;
using FamiliesWebAPI.Data;
using FamiliesWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamiliesWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        
        [HttpGet]
        public async Task<ActionResult<User>> ValidateUser([FromQuery] string username, [FromQuery] string password)
        {
            try
            {
                var user = await userService.ValidateUserAsync(username, password);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}