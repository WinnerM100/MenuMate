

using MenuMate.Models;

namespace MenuMate.Middleware.Security;

public class MethodAuthorizationCollection
{
    private IConfiguration configuration;

    public List<MethodAuthorization> methodAuthorizations;

    public MethodAuthorizationCollection(IConfiguration configuration)
    {
        methodAuthorizations = new List<MethodAuthorization>();
        this.configuration = configuration;

        IEnumerable<IConfigurationSection> methodAuthorizationSection = configuration.GetSection("Authorization").GetSection("Methods").GetChildren();

         foreach(IConfigurationSection methodAuth in methodAuthorizationSection)
         {
            List<string> allowedRoles = new List<string>();

            foreach(IConfigurationSection allowedRoleSection in methodAuth.GetSection("AllowedRoles").GetChildren())
            {
                allowedRoles.Add(allowedRoleSection.Get<string>());
            }

            string endpointName = methodAuth.GetSection("EndpointName").Value ?? "";
            string httpMethod = methodAuth.GetSection("HttpMethod").Value ?? "";

            methodAuthorizations.Add(new MethodAuthorization(endpointName, httpMethod, allowedRoles));
         }
    }


}