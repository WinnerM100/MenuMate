

using MenuMate.Extensions;
using MenuMate.Models;
using MenuMate.Models.DAOs;
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

    [HttpGet]
    public IActionResult GetUserByUsernameAndPassword([FromQuery]string username, [FromQuery]string password)
    {
        UserDAO? foundUser = userService.GetUserByEmailAndPassword(username,password);

        if (foundUser == null)
        {
            return NotFound($"No User Found for {{ username: '{username}', password: '{password}' }}");
        }
        else return Ok(foundUser);

    }

    [HttpPut]
    public IActionResult UpdateUser(UserDTO userDTO, [FromQuery]Guid userId)
    {
        UserDAO result = userService.UpdateUserById(userDTO, userId);

        if (result == null)
        {
            return NotFound($"No User Found with details: {{UserId: {userId}}}");
        }

        return CreatedAtAction(nameof(UpdateUser), result);
    }

    [HttpDelete]
    public IActionResult DeleteUser([FromQuery]string username, [FromQuery]string password)
    {
        UserDAO result = userService.DeleteUserByEmailAndPassword(username, password);

        if (result == null)
        {
            return NotFound($"No User Found with details: {{{result}}}");
        }

        return CreatedAtAction(nameof(UpdateUser), result);
    }
}