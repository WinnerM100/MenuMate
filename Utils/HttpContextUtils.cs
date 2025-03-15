

using MenuMate.Models;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace MenuMate.Utils;
public static class HttpContextUtils
{
    public static MethodAuthorization GetMethodDetailsFromHttpContext(HttpContext context)
    {
        Endpoint? endpoint = context.GetEndpoint();
        
        if(endpoint == null)
        {
            return new MethodAuthorization();
        }

        EndpointMetadataCollection metadataColl = endpoint.Metadata;
        string? methodCall = ((ControllerActionDescriptor)metadataColl.FirstOrDefault(m => m.GetType().Name.Equals(nameof(ControllerActionDescriptor))))?.ActionName;

        IReadOnlyList<string> httpMethods =((HttpMethodMetadata)metadataColl.FirstOrDefault(m => m.GetType().Name.Equals(nameof(HttpMethodMetadata))))?.HttpMethods ?? new List<string>();

        if(httpMethods == null || httpMethods.Count() == 0)
        {
            return new MethodAuthorization();
        }

        string httpVerb = httpMethods[0];

        return new MethodAuthorization(methodCall, httpVerb, new List<string>());
    }
}