using HumanCapitalManagement.Persistance.Repositories;
using HumanCapitalManagement.Service.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumanCapitalManagement.API.DependencyInjection.Address;
public static class AddressServiceCollectionExtensions
{
    public static IServiceCollection AddAddressServices(this IServiceCollection services)
    {
        services.TryAddScoped<IAddressRepo, AddressRepo>();
        services.TryAddScoped<IAddressService, AddressService>();

        return services;
    }
}
