

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MenuMate.Models;
using MenuMate.Models.DTOs;
using MenuMate.Security.Authentication;

namespace MenuMate.Services;

public class AuthService : IAuthService
{
    private readonly IUserService userService;

    private readonly IAuthenticator authenticator;

    public AuthService(IUserService userService, IAuthenticator authenticator)
    {
        this.userService = userService;
        this.authenticator = authenticator;
    }

    public string GetTokenForUser(UserDTO userDTO)
    {
        throw new NotImplementedException();
    }

    public string Login(LoginDTO loginDetails)
    {
        User userToLogin = userService.GetUserByEmailAndPassword(loginDetails.Email, loginDetails.Password);

        if(userToLogin == null)
        {
            return "";
        }

        string jwtToken = authenticator.WriteToken(authenticator.GenerateJwtToken(userToLogin));

        return jwtToken;
    }

    
}