
using System.Security;
using Microsoft.AspNetCore.Authorization;

namespace MenuMate.Security.Authorization.Attributes;

public sealed class HasRoleAttribute : AuthorizeAttribute
{
    public HasRoleAttribute() : base(policy: "authenticationRequired")
    {
        
    }
}