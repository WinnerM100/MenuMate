using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MenuMate.Constants.Exceptions;
using MenuMate.Context;
using MenuMate.DTOs;
using MenuMate.Models;
using MenuMate.Utilities;
using MenuMate.Utilities.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MenuMate.Services;

public class UserService : IUserService
{
    SqlConnector connector;
    ClientContext context;
    IConfiguration configuration;

    private readonly string tokenKey;
    private readonly string jwtIssuer;
    private readonly string jwtAudience;

    private readonly string hashAlgorithm;

    private readonly string tokenExpiresInMinutes;

    public UserService(SqlConnector newConnector, ClientContext newContext, IConfiguration configuration)
    {
        connector = newConnector;
        context = newContext;
        this.configuration = configuration;

        tokenKey = configuration.GetSection("Jwt").GetValue<string>("Key");
        jwtIssuer = configuration.GetSection("Jwt").GetValue<string>("Issuer");
        jwtAudience = configuration.GetSection("Jwt").GetValue<string>("Audience");
        tokenExpiresInMinutes = configuration.GetSection("Jwt").GetValue<string>("ExpiresInMinutes");

        hashAlgorithm = configuration.GetSection("Security").GetValue<string>("HashAlgorithm");
    }

    public User GetUser(UserDAO loginUser)
    {
        throw new NotImplementedException();
    }

    public User GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public string LoginUser(UserDTO userDTO)
    {
        string hashedPassword = SaltHash.ComputeHash(userDTO.Password, hashAlgorithm);
        List<User> users = context.users.Where(u => u.Email.ToLower().Equals(userDTO.Email.ToLower()) && hashedPassword.Equals(u.Password)).ToList();

        if(users == null || users.Count() == 0)
        {
            throw new NotFoundException(nameof(users), userDTO.ToString());
        }

        if(users.Count() > 1)
        {
            throw new Exception($"Multiple user detected when queried the input {userDTO}");
        }

        User targetUser = users.First();
        
        string jwtToken = Generate(targetUser);

        return jwtToken;
    }
    public UserDAO CreateUser(UserDTO userDTO, Role role)
    {   
        List<Role> roles = new List<Role>
        {
            role
        };
        User newUser = new User(roles)
        {
            Email = userDTO.Email,
            Password = SaltHash.ComputeHash(userDTO.Password, hashAlgorithm)
        };
        context.users.Add(newUser);

        return newUser.AsUserDAO();
    }
    private string Generate(User user)
    {
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        SigningCredentials credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
            SaltHash.GetAlgorithmSignature(hashAlgorithm)
        );

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {   
            Issuer = jwtIssuer,
            Audience = jwtAudience,
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(tokenExpiresInMinutes)),
            SigningCredentials = credentials
        };

        SecurityToken token = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
        return jwtSecurityTokenHandler.WriteToken(token);
    }

    private ClaimsIdentity GenerateClaims(User user)
    {
        ClaimsIdentity claims = new ClaimsIdentity();

        claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));

        foreach (Role role in user.Roles)
        {
            claims.AddClaim(new Claim(ClaimTypes.Role, role.Value));
        }

        return claims;
    }
}