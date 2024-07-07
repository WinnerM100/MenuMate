
using System.Collections;
using MenuMate.Constants.Exceptions;
using MenuMate.Models.Config;

namespace MenuMate.Utilities;

public class RolesSettings
{
    private List<AllowedRole> ConfigRoles { get; init; }

    public RolesSettings(IConfiguration configuration)
    {
        ConfigRoles = configuration.GetSection("Authorization")
                                   .GetSection("AllowedRoles")
                                   .Get<List<AllowedRole>>();
    }

    private List<AllowedRole> GetRolesFromConfig()
    {
        return ConfigRoles.ToList();
    }

    public AllowedRole GetRoleByKey(string roleKey)
    {
        var targetRoleConfig = ConfigRoles.Where(configRole => configRole.Name.Equals("CLIENT")).FirstOrDefault();

        if(targetRoleConfig == null)
        {
            throw new NotFoundException("roleConfig");
        }

        return targetRoleConfig;
    }
}