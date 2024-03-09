using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace HumanCapitalManagement.Utilities.Authorization;
public static class TokenHelper
{
    public static SigningCredentials GetSigningCredentials(string clientSecret)
    {
        var key = Encoding.UTF8.GetBytes(clientSecret);
        var securityKey = new SymmetricSecurityKey(key);
        var signingCreds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        return signingCreds;
    }

    public static SecurityTokenDescriptor DescribeToken(string clientId, SigningCredentials signingCreds)
    {
        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", clientId) }),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = signingCreds
        };
    }
}
