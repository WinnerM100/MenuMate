

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MenuMate.Models;
using Microsoft.IdentityModel.Tokens;

namespace MenuMate.Security.Authentication;

public class JwtAuthenticator : IAuthenticator
{
    private string SecurityKey { get; init; }
    private string Issuer { get; init; }
    private string Audience { get; init; }
    private int ExpiresInMinutes { get; init; }
    private string HashAlgorithm { get; init; }

    public JwtAuthenticator(IConfiguration configuration)
    {
        SecurityKey = configuration.GetSection("Jwt").GetValue<string>("Key");
        Issuer = configuration.GetSection("Jwt").GetValue<string>("Issuer");
        Audience = configuration.GetSection("Jwt").GetValue<string>("Audience");
        ExpiresInMinutes = configuration.GetSection("Jwt").GetValue<int>("ExpiresInMinutes");
        HashAlgorithm = configuration.GetSection("Security").GetValue<string>("HashAlgorithm");
    }
    public JwtSecurityToken GenerateJwtToken(User user)
    {
        List<Claim> userClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach(Role role in user.Roles)
        {
            userClaims.Add(new Claim(ClaimTypes.Role, role.Value));
        }

        return GenerateToken(userClaims);
    }

    private JwtSecurityToken GenerateToken(List<Claim> claims)
    {
        SymmetricSecurityKey authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            expires: DateTime.Now.AddMinutes(ExpiresInMinutes),
            claims: claims,
            signingCredentials: new SigningCredentials(authKey,GetSecurityAlgorithm())
        );

        return token;
    }

    private string GetSecurityAlgorithm()
    {
        switch(HashAlgorithm.ToUpper())
        {
            case "HMACSHA256":
                return SecurityAlgorithms.HmacSha256;
            default:
                return "";
        }
    }

    public string WriteToken(JwtSecurityToken token)
    {
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}