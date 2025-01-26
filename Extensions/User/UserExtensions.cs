using MenuMate.DTOs;
using MenuMate.Models;

public static class UserExtensions
{
    public static UserDTO AsUserDTO(this User user, bool maskId = true)
    {
        return new UserDTO
        {
            Id = maskId?Guid.NewGuid():user.Id,
            Email = user.Email,
            Password = user.Password
        };
    }
    public static UserDAO AsUserDAO(this User user, bool maskId = true)
    {
        return new UserDAO
        {
            Id = maskId?Guid.NewGuid():user.Id,
            Email = user.Email,
            Password = user.Password
        };
    }
}