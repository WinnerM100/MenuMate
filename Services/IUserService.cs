
using MenuMate.Models;

namespace MenuMate.Services;

public interface IUserService
{
    public User GetUser(UserDAO loginUser);

    public User GetUserByEmail(string email);
}