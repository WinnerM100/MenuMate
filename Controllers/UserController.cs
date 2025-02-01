

using MenuMate.Extensions;
using MenuMate.Models;
using MenuMate.Models.DTOs;
using MenuMate.Services;
using Microsoft.AspNetCore.Mvc;

namespace MenuMate.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService userService { get; set;}

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost("{clientId}")]
    public IActionResult CreateUserForClient(Guid clientId,[FromBody] UserDTO userDetails)
    {
        User? createdUser = userService.CreateUserForClient(clientId, userDetails);

        if (createdUser == null)
        {
            return NotFound($"No Client Found for Id:'{clientId}'");
        }

        return Ok(createdUser.ToUserDTO());
    }
}