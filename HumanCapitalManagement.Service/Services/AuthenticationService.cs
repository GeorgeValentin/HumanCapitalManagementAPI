using HumanCapitalManagement.Utilities.Authorization;
using HumanCapitalManagement.Utilities.AzureKeyVault;
using HumanCapitalManagement.Utilities.Cache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace HumanCapitalManagement.Service.Services;
public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly IAzureKeyVaultService _azureKeyVaultService;
    private readonly IMemoryCache _memoryCache;

    public AuthenticationService(
        IConfiguration configuration,
        IAzureKeyVaultService azureKeyVaultService,
        IMemoryCache memoryCache)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _azureKeyVaultService = azureKeyVaultService ?? throw new ArgumentNullException(nameof(azureKeyVaultService));
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public async Task<string>? CreateTokenFromAzureKeyVault(TokenRequest request)
    {
        var cacheKey = "keyVaultValues";

        if (!_memoryCache.TryGetValue(cacheKey, out KeyVaultValues keyVaultValues))
        {
            keyVaultValues = await _azureKeyVaultService
                .GetKeyVaultSecretAsync(
                    clientIdKey: _configuration["Azure_Key_Vault:selectors:clientIdKey"],
                    clientSecretKey: _configuration["Azure_Key_Vault:selectors:clientSecretKey"],
                    grantTypeKey: _configuration["Azure_Key_Vault:selectors:grantTypeKey"],
                    keyVaultUri: _configuration["Azure_Key_Vault:KeyVaultURI"]);

            CacheHelper.SetMemoryCache(cacheKey, keyVaultValues, _memoryCache);
        }

        if (
            request.ClientId.Equals(keyVaultValues.ClientId!.Value) &&
            request.ClientSecret.Equals(keyVaultValues.ClientSecret!.Value) &&
            request.GrantType.Equals(keyVaultValues.GrantType!.Value))
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            SigningCredentials signingCreds = TokenHelper.GetSigningCredentials(request.ClientSecret);
            SecurityTokenDescriptor tokenDescriptor = TokenHelper.DescribeToken(request.ClientId, signingCreds);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenResult = tokenHandler.WriteToken(token);

            return tokenResult;
        }

        return null!;
    }

    public string? CreateTokenLocally(TokenRequest request)
    {
        if (request.ClientId.Equals(_configuration["JwtCreds:client-id"]) &&
            request.ClientSecret.Equals(_configuration["JwtCreds:client-secret"]) &&
            request.GrantType.Equals(_configuration["JwtCreds:grant-type"]))
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            SigningCredentials signingCreds = TokenHelper.GetSigningCredentials(request.ClientSecret);
            SecurityTokenDescriptor tokenDescriptor = TokenHelper.DescribeToken(request.ClientId, signingCreds);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenResult = tokenHandler.WriteToken(token);

            return tokenResult;
        }
        return null;
    }
}
