using HumanCapitalManagement.Persistance.Repositories;
using HumanCapitalManagement.Service.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumanCapitalManagement.API.DependencyInjection.Contract;
public static class ContractServiceCollectionExtensions
{
    public static IServiceCollection AddContractServices(this IServiceCollection services)
    {
        services.TryAddScoped<IContractRepo, ContractRepo>();
        services.TryAddScoped<IContractService, ContractService>();

        return services;
    }
}
