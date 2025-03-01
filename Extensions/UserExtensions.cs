using MenuMate.Models;
using MenuMate.Models.DAOs;
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
            Password = user.Password,
            
        };
    }
    public static UserDAO ToUserDAO(this User user, bool maskUserId = true)
    {
        return new UserDAO() with
        {
            Id = maskUserId ? Guid.Empty : user.Id,
            Email = user.Email,
            Password = user.Password,
        };
    }

    public static bool IsEqual(this User user, User other, bool fullCheck = true)
    {
        if(other == null)
        {
            return false;
        }

        if(fullCheck)
            return user.Id == other.Id && user.Email.ToLower().Equals(other.Email.ToLower()) && user.Password.Equals(other.Password);

        if(other.Id != null && other.Id != user.Id)
        {
            return false;
        }

        if(!string.IsNullOrEmpty(user.Email) && !user.Email.ToLower().Equals(other.Email.ToLower()))
        {
            return false;
        }

        if(!string.IsNullOrEmpty(user.Password) && !user.Password.Equals(other.Password))
        {
            return false;
        }

        return true;
    }
}