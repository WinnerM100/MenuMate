using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuMate.Controllers.Authentication
{
    [ApiController]
    [Route("/")]
    class AuthController : ControllerBase
    {
        [HttpGet("/secret")]
        [Authorize(Roles = "admin")]
        public ActionResult<string> GetSecret()
        {
            return "This is the everlooking secret";
        }

        // [HttpGet("/login")]
        // [AllowAnonymous]
        // public IActionResult Login()
        // {
            
        // }
    }
}