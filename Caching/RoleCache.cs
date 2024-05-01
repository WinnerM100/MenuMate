

using MenuMate.Constants.Exceptions;
using MenuMate.Models;
using MenuMate.Services;

namespace MenuMate.Caching;

public class RoleCache
{
    private List<Role> AvailableRoles { get; set; }
    private IRoleService roleService{ get; set; }

    public RoleCache(IRoleService newRoleService)
    {
        roleService = newRoleService;
    }

    public IEnumerable<Role> GetAvailableRoles()
    {
        return null == AvailableRoles || AvailableRoles.Count == 0?roleService.GetRoles():AvailableRoles;
    }

    public Role GetRole(string roleName)
    {
        if(null == AvailableRoles || AvailableRoles.Count == 0)
        {
            AvailableRoles = GetAvailableRoles().ToList();
        }

        Role targetRole = AvailableRoles.Where(role => roleName.Equals(role.Name)).FirstOrDefault();

        if(targetRole == null)
        {
            throw new NotFoundException("role", $"roleName = {roleName}");
        }

        return targetRole;
    }
}