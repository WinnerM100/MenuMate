

using MenuMate.Context;
using MenuMate.Models;
using MenuMate.Utilities.Sql;

namespace MenuMate.Services;

public class RoleService : IRoleService
{
    SqlConnector sqlConnector;

    ClientContext context; 

    private readonly IServiceProvider serviceProvider;

    public static IEnumerable<Role> Roles { get; set; }

    public RoleService(SqlConnector sqlConnector, ClientContext context)
    {
        this.sqlConnector = sqlConnector;
        this.context = context;
    }

    public IEnumerable<Role> GetRoles()
    {
       return context.roles.ToList();
    }
}