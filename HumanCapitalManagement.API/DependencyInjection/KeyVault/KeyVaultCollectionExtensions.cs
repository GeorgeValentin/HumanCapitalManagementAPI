using HumanCapitalManagement.Service.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumanCapitalManagement.API.DependencyInjection.KeyVault;

public static class KeyVaultCollectionExtensions
{
    public static IServiceCollection AddKeyVaultServices(this IServiceCollection services)
    {
        services.TryAddScoped<IAzureKeyVaultService, AzureKeyVaultService>();
        
        return services;
    }
}
