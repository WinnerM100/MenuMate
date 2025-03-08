

using MenuMate.Models;
using MenuMate.Models.DTOs;

public interface IRoleService
{
    public void PopulateRoleTableFromConfig();

    public IEnumerable<Role> GetRolesForUser(UserDTO userDTO);
    public Role GetRoleByName(string roleName);
}