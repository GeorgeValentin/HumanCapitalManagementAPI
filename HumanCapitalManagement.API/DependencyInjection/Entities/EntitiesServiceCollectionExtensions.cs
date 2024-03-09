using HumanCapitalManagement.Persistance.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumanCapitalManagement.API.DependencyInjection;
public static class EntitiesServiceCollectionExtensions
{
    public static IServiceCollection AddEntitiesServices(this IServiceCollection services)
    {
        services.TryAddScoped<IEntitiesRepo, EntitiesRepo>();

        return services;
    }
}
