

namespace MenuMate.Models;

public class MethodAuthorization
{

    public MethodAuthorization(string endpointName, string httpMethod, List<string> allowedRoles)
    {
        EndpointName = endpointName;
        this.HttpMethod = httpMethod;
        AllowedRoles = allowedRoles;
    }
    public MethodAuthorization()
    {
    }

    public string EndpointName { get; set; }
    public string HttpMethod { get; set;}
    public List<string> AllowedRoles { get; set; } = new List<string>();
}