using HumanCapitalManagement.Service.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumanCapitalManagement.API.DependencyInjection.AzureSvBus;

public static class AzureServicBusCollectionExtensions
{
    public static IServiceCollection AddAzureServiceBusServices(this IServiceCollection services)
    {
        services.TryAddTransient<IAzureServiceBus, AzureServiceBus>();

        return services;
    }
}
