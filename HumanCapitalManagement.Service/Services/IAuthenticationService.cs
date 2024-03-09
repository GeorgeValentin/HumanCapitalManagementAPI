using HumanCapitalManagement.Utilities.Authorization;

namespace HumanCapitalManagement.Service.Services;
public interface IAuthenticationService
{
    Task<string>? CreateTokenFromAzureKeyVault(TokenRequest request);
    string? CreateTokenLocally(TokenRequest request);
}
