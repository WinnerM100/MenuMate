using MenuMate.Models;
using MenuMate.Models.DTOs;

namespace MenuMate.Extensions;

public static class UserDTOExtensions
{
    public static User ToUser(this UserDTO user, bool createUserId = true)
    {
        return new User()
        {
            Id = (Guid)(createUserId ? Guid.NewGuid() : (user.Id == null || Guid.Empty == user.Id? Guid.Empty : user.Id)),
            Email = user.Email,
            Password = user.Password
        };
    }
}