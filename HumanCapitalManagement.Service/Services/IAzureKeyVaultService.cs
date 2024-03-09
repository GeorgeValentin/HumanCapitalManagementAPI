using HumanCapitalManagement.Utilities.AzureKeyVault;

namespace HumanCapitalManagement.Service.Services;
public interface IAzureKeyVaultService
{
    Task<KeyVaultValues> GetKeyVaultSecretAsync(
        string clientIdKey,
        string clientSecretKey,
        string grantTypeKey,
        string keyVaultUri);
}
