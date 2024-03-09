using Azure.Security.KeyVault.Secrets;

namespace HumanCapitalManagement.Utilities.AzureKeyVault;
public class KeyVaultValues
{
    public KeyVaultSecret? ClientId { get; set; }
    public KeyVaultSecret? ClientSecret { get; set; }
    public KeyVaultSecret? GrantType { get; set; }
}
