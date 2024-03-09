using HumanCapitalManagement.Persistance.Repositories;
using HumanCapitalManagement.Service.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumanCapitalManagement.API.DependencyInjection.Degree;

public static class DegreeCollectionExtensions
{
    public static IServiceCollection AddDegreeServices(this IServiceCollection services)
    {
        services.TryAddScoped<IDegreeRepo, DegreeRepo>();
        services.TryAddScoped<IDegreeService, DegreeService>();

        return services;
    }
}
