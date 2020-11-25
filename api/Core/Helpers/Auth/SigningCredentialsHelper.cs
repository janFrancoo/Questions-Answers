using Microsoft.IdentityModel.Tokens;

namespace Core.Helpers.Auth
{
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }
    }
}
