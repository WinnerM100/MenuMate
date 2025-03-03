

using MenuMate.AccessLayer.Context;
using MenuMate.Models;
using MenuMate.Models.DTOs;

public class RoleService : IRoleService
{

    private readonly IConfiguration _configuration;

    private MenuMateContext _dbContext { get; set; }
    public RoleService(IConfiguration configuration, MenuMateContext dbContext)
    {
        this._configuration = configuration;
        this._dbContext = dbContext;
    }

    public IEnumerable<Role> GetRolesForUser(UserDTO userDTO)
    {
        throw new NotImplementedException();
    }

    public void PopulateRoleTableFromConfig()
    {
        List<Role> configRoles = new List<Role>();

        var roles = _configuration.GetSection("Authorization").GetSection("AllowedRoles").GetChildren();

        foreach(IConfigurationSection role in roles)
        {
            configRoles.Add(new Role()
            {
                Id = Guid.NewGuid(),
                Name = role.GetSection("Name").Value ?? "",
                Value = role.GetSection("Value").Value ?? "",
            });
        }

        foreach(Role role in configRoles)
        {
            if(!_dbContext.Roles.Where(r => r.Name.Equals(role.Name) && r.Value.Equals(role.Value)).Any())
            {
                _dbContext.Roles.Add(role);
            }
        }

        _dbContext.SaveChanges();
    }
}