using MenuMate.Models;
using MenuMate.Models.DTOs;

namespace MenuMate.Extensions;

public static class UserExtensions
{
    public static UserDTO ToUserDTO(this User user, bool maskUserId = true)
    {
        return new UserDTO() with
        {
            Id = maskUserId ? Guid.Empty : user.Id,
            Email = user.Email,
            Password = user.Password
        };
    }
}