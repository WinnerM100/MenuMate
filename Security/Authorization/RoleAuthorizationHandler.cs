
using System.Security.Claims;
using MenuMate.Middleware.Security;
using MenuMate.Models;
using MenuMate.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Net.Http.Headers;

namespace MenuMate.Security.Authorization;

public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
{
    MethodAuthorizationCollection AuthorizationsForMethods { get; init; }

    public RoleAuthorizationHandler(MethodAuthorizationCollection authorizationsForMethods)
    {
        this.AuthorizationsForMethods = authorizationsForMethods;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        HashSet<string> roles = context.User.Claims.Where(claim => claim.Type == ClaimTypes.Role).Select(claim => claim.Value).ToHashSet();

        if(roles == null || roles.Count == 0)
        {
            return;
        }

        if(context.Resource is null)
        {
            return;
        }

        HttpContext httpContext = context.Resource as HttpContext;

        MethodAuthorization currentMethodState = HttpContextUtils.GetMethodDetailsFromHttpContext(httpContext);

        currentMethodState.AllowedRoles = AuthorizationsForMethods.methodAuthorizations
                                          .FirstOrDefault( auth => auth.HttpMethod.Equals(currentMethodState.HttpMethod) 
                                                        && auth.EndpointName.Equals(currentMethodState.EndpointName))?
                                          .AllowedRoles ?? new List<string>();
        if(currentMethodState.AllowedRoles == null || currentMethodState.AllowedRoles.Count == 0)
        {
            context.Fail(new AuthorizationFailureReason(this, $"Endpoint '{currentMethodState.EndpointName}' cannot be accessed by any user!"));
        }

        bool userIsAuthorisedToAccessMethod = IsUserAuthorizedForRequest(roles, currentMethodState);

        if(userIsAuthorisedToAccessMethod)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail(new AuthorizationFailureReason(this, $"Endpoint '{currentMethodState.EndpointName}' cannot be accessed this user."));
        }
    }
    private bool IsUserAuthorizedForRequest(HashSet<string> currentUserRoles, MethodAuthorization authorization)
    {
        foreach(string allowedRole in authorization.AllowedRoles)
        {
            if(currentUserRoles.Contains(allowedRole))
            {
                return true;
            }
        }
        return false;
    }
}