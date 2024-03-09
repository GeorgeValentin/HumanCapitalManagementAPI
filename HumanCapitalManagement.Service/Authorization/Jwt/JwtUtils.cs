using HumanCapitalManagement.Service.Services;
using HumanCapitalManagement.Utilities.AzureKeyVault;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace HumanCapitalManagement.Service.Authorization.Jwt;
public class JwtUtils : IJwtUtils
{
    private readonly IMemoryCache _memoryCache;
    private readonly IAzureKeyVaultService _azureKeyVaultService;
    private readonly IConfiguration _configuration;

    public JwtUtils(
        IMemoryCache memoryCache,
        IAzureKeyVaultService azureKeyVaultService,
        IConfiguration configuration)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _azureKeyVaultService = azureKeyVaultService ?? throw new ArgumentNullException(nameof(azureKeyVaultService));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<int?> ValidateTokenProduction(string token)
    {
        if (token == null)
        {
            return null;
        }

        var cacheKey = "keyVaultValues";

        _memoryCache.TryGetValue(cacheKey, out KeyVaultValues keyVaultValues);

        if (keyVaultValues == null)
        {
            keyVaultValues = await _azureKeyVaultService
                .GetKeyVaultSecretAsync(
                    clientIdKey: _configuration["Azure_Key_Vault:selectors:clientIdKey"],
                    clientSecretKey: _configuration["Azure_Key_Vault:selectors:clientSecretKey"],
                    grantTypeKey: _configuration["Azure_Key_Vault:selectors:grantTypeKey"],
                    keyVaultUri: _configuration["Azure_Key_Vault:KeyVaultURI"]);
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(keyVaultValues.ClientSecret!.Value);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var clientId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            return clientId;
        }
        catch
        {
            return null;
        }
    }

    public int? ValidateToken(string token)
    {
        if (token == null)
            return null;
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JwtCreds:client-secret"]);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var clientId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            return clientId;
        }
        catch
        {
            return null;
        }
    }
}
