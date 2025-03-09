

using MenuMate.Models.DTOs;

namespace MenuMate.Services;

public class AuthService : IAuthService
{
    private readonly IUserService userService;

    public AuthService(IUserService userService)
    {
        this.userService = userService;
    }

    public string GetTokenForUser(UserDTO userDTO)
    {
        throw new NotImplementedException();
    }
}