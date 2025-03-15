

using Microsoft.AspNetCore.Authorization;

namespace MenuMate.Security.Authorization;

public class RoleRequirement : IAuthorizationRequirement
{
    public string PolicyName { get; init; }

    public RoleRequirement(string policyName)
    {
        PolicyName = policyName;
    }
}