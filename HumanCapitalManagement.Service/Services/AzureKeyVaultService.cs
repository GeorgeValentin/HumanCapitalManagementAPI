using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using HumanCapitalManagement.Utilities.AzureKeyVault;

namespace HumanCapitalManagement.Service.Services;
public class AzureKeyVaultService : IAzureKeyVaultService
{
    public async Task<KeyVaultValues> GetKeyVaultSecretAsync(
        string clientIdKey, 
        string clientSecretKey,
        string grantTypeKey,
        string keyVaultUri)
    {
        var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());

        var clientIdValue = await client.GetSecretAsync(clientIdKey);
        var clientSecretValue = await client.GetSecretAsync(clientSecretKey);
        var grantTypeValue = await client.GetSecretAsync(grantTypeKey);

        return new KeyVaultValues
        {
            ClientId = clientIdValue,
            ClientSecret = clientSecretValue,
            GrantType = grantTypeValue
        };
    }
}
