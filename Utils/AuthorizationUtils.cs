
using System.IdentityModel.Tokens.Jwt;
using MenuMate.Constants.Enums;

namespace MenuMate.Utils
{
    public class AuthorizationUtils
    {
        public static string GetRoleFromAuthorization(string authToken, AuthenticationTypes authType)
        {
            switch(authType)
            {
                case AuthenticationTypes.JwtAuth:
                    return GetRoleFromJwtToken(authToken);
                default:
                    throw new ArgumentException($"Unknown authentication! Cannot handle: {authType}");
            }
        }

        private static string GetRoleFromJwtToken(string token)
        {
            string jwtToken = token.Replace("Bearer ", "");

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwt = jwtSecurityTokenHandler.ReadJwtToken(token);

            Console.WriteLine(jwt);

            return jwtToken;
        }
    }
}