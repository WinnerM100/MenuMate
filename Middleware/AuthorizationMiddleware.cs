
using MenuMate.Models;
using MenuMate.Utils;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Net.Http.Headers;

namespace MenuMate.Middleware.Security;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate next;
    private readonly MethodAuthorizationCollection authorizations;

    public AuthorizationMiddleware(RequestDelegate nextMiddleware, MethodAuthorizationCollection authorizations)
    {
        this.next = nextMiddleware;
        this.authorizations = authorizations;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        bool isUserAuthorized = IsUserAuthorizedForRequest(context);

        if(!isUserAuthorized)
        {
            throw new UnauthorizedAccessException("You are not allowed to access this request!");
        }

        await next(context);
    }

    public bool IsUserAuthorizedForRequest(HttpContext context)
    {
        Endpoint endpoint = context.GetEndpoint();
        
        if(endpoint == null)
        {
            return true;
        }

        EndpointMetadataCollection metadataColl = endpoint.Metadata;
        string methodCall = ((ControllerActionDescriptor)metadataColl.FirstOrDefault(m => m.GetType().Name.Equals(nameof(ControllerActionDescriptor))))?.ActionName;

        IReadOnlyList<string> httpMethods =((HttpMethodMetadata)metadataColl.FirstOrDefault(m => m.GetType().Name.Equals(nameof(HttpMethodMetadata))))?.HttpMethods;

        if(httpMethods == null || httpMethods.Count() == 0)
        {
            return true;
        }

        string httpVerb = httpMethods[0];
        string token = context.Request.Headers[HeaderNames.Authorization];

        string currentUserRole = AuthorizationUtils.GetRoleFromAuthorization(token, Constants.Enums.AuthenticationTypes.JwtAuth);

        foreach (MethodAuthorization methodAuthorization in authorizations.methodAuthorizations)
        {
            if(methodAuthorization.EndpointName.Equals(methodCall) && methodAuthorization.HttpMethod.Equals(httpVerb))
            {
                return true;
            }
        }
        return false;
    }
}

public static class AuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthorizationMiddleware>();
    }
}