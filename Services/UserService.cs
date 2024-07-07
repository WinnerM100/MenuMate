using MenuMate.Context;
using MenuMate.Models;
using MenuMate.Utilities.Sql;

namespace MenuMate.Services;

public class UserService : IUserService
{
    SqlConnector connector;
    ClientContext context;

    public UserService(SqlConnector newConnector, ClientContext newContext)
    {
        connector = newConnector;
        context = newContext;
    }

    public User GetUser(UserDAO loginUser)
    {
        throw new NotImplementedException();
    }

    public User GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }
}