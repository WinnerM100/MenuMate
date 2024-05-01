

using MenuMate.Context;
using MenuMate.Models;
using MenuMate.Utilities.Sql;

namespace MenuMate.Services;

public class RoleService : IRoleService
{
    SqlConnector sqlConnector;

    AuthContext authContext; 

    private readonly IServiceProvider serviceProvider;

    public static IEnumerable<Role> Roles { get; set; }

    public RoleService(SqlConnector sqlConnector, AuthContext authContext)
    {
        this.sqlConnector = sqlConnector;
        this.authContext = authContext;
    }

    public IEnumerable<Role> GetRoles()
    {
       return authContext.roles.ToList();
    }
}