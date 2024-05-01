using MenuMate.Context;
using MenuMate.Models;
using MenuMate.Utilities.Sql;

namespace MenuMate.Services;

public class UserService : IUserService
{
    SqlConnector connector;
    AuthContext authContext;

    public UserService(SqlConnector newConnector, AuthContext newAuthContext)
    {
        connector = newConnector;
        authContext = newAuthContext;
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