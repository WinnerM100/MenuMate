
using MenuMate.Models;
using MenuMate.Models.DAOs;

namespace MenuMate.Extensions;
public static class RoleExtensions
{
    public static RoleDAO ToRoleDAO(this Role role, bool maskRoleId = true)
    {
        return new RoleDAO
        {
            Id = maskRoleId? Guid.Empty: role.Id,
            Name = role.Name,
            Value = role.Value
        };
    }
}