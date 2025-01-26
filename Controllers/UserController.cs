using System.Security.Claims;
using System.Text;
using MenuMate.DTOs;
using MenuMate.Models;
using MenuMate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MenuMate.Controllers.Authentication
{
    [ApiController]
    [Route("/user")]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/signin")]
        public IActionResult Login([FromBody] UserDTO userDTO)
        {   
            try
            {
                var loggedUser = userService.LoginUser(userDTO);

                return String.IsNullOrEmpty(loggedUser)? NotFound(loggedUser):Ok(loggedUser);
            }
            catch (Exception ex)
            {   Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return BadRequest(userDTO);
            }
        }

    }
}