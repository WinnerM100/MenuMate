

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MenuMate.Models;

namespace MenuMate.Security.Authentication;

public interface IAuthenticator
{
    public JwtSecurityToken GenerateJwtToken(User user);

    public string WriteToken(JwtSecurityToken token);
}