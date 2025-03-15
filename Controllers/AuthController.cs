

using MenuMate.Models;
using MenuMate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MenuMate.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public IActionResult LoginUser(LoginDTO loginDTO)
    {
        string generatedToken = _authService.Login(loginDTO);

        if(string.IsNullOrEmpty(generatedToken))
        {
            return NotFound($"No User found for Credentials: {loginDTO}");
        }

        return Ok(generatedToken);
    }
}