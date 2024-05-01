using MenuMate.Models;

namespace MenuMate.Services;

public interface IRoleService
{
    public IEnumerable<Role> GetRoles();
}