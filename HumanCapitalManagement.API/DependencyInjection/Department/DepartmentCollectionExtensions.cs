using HumanCapitalManagement.Persistance.Repositories;
using HumanCapitalManagement.Service.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumanCapitalManagement.API.DependencyInjection.Department;

public static class DepartmentCollectionExtensions
{
    public static IServiceCollection AddDepartmentServices(this IServiceCollection services)
    {
        services.TryAddScoped<IDepartmentRepo, DepartmentRepo>();
        services.TryAddScoped<IDepartmentService, DepartmentService>();

        return services;
    }
}
