
using MenuMate.Models;
using MenuMate.Models.DAOs;
using MenuMate.Models.DTOs;

namespace MenuMate.Extensions;
public static class UserDAOExtensions
{
    public static User ToUser(this UserDAO user, bool createUserId = true)
    {
        return new User()
        {
            Id = (Guid)(createUserId ? Guid.NewGuid() : (user.Id == null || Guid.Empty == user.Id? Guid.Empty : user.Id)),
            Email = user.Email,
            Password = user.Password
        };
    }
}