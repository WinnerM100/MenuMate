
using MenuMate.Models;
using Microsoft.AspNetCore.Mvc.Controllers;

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
        Endpoint endpoint = context.GetEndpoint();
        
        if(endpoint == null)
        {
            return;
        }

        EndpointMetadataCollection metadataColl = endpoint.Metadata;
        string methodCall = ((ControllerActionDescriptor)metadataColl.FirstOrDefault(m => m.GetType().Name.Equals(nameof(ControllerActionDescriptor))))?.ActionName;

        IReadOnlyList<string> httpMethods =((HttpMethodMetadata)metadataColl.FirstOrDefault(m => m.GetType().Name.Equals(nameof(HttpMethodMetadata))))?.HttpMethods;

        if(httpMethods == null || httpMethods.Count() == 0)
        {
            return;
        }

        string httpVerb = httpMethods[0];

        foreach (MethodAuthorization methodAuthorization in authorizations.methodAuthorizations)
        {
            if(methodAuthorization.EndpointName == methodCall && methodAuthorization.HttpMethod == httpVerb && methodAuthorization.AllowedRoles.Contains())
        }
    }
}

public static class AuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthorizationMiddleware>();
    }
}