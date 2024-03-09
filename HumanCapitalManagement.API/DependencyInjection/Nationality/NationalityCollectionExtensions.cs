using HumanCapitalManagement.Persistance.Repositories;
using HumanCapitalManagement.Service.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumanCapitalManagement.API.DependencyInjection.Nationality;

public static class DepartmentCollectionExtensions
{
    public static IServiceCollection AddNationalitiesServices(this IServiceCollection services)
    {
        services.TryAddScoped<INationalitiesService, NationalitiesService>();
        services.TryAddScoped<INationalityRepo, NationalityRepo>();

        return services;
    }
}
