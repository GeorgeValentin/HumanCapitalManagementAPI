using HumanCapitalManagement.Persistance.Repositories;
using HumanCapitalManagement.Service.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumanCapitalManagement.API.DependencyInjection.Employee;
public static class EmployeeServiceCollectionExtensions
{
    public static IServiceCollection AddEmployeeServices(this IServiceCollection services)
    {
        services.TryAddScoped<IEmployeeRepo, EmployeeRepo>();
        services.TryAddScoped<IEmployeeSkillRepo, EmployeeSkillRepo>();
        services.TryAddScoped<IEmployeeStudyProgramRepo, EmployeeStudyProgramRepo>();

        services.TryAddScoped<IEmployeeService, EmployeesService>();
        services.TryAddScoped<IEmployeeSkillService, EmployeeSkillService>();
        services.TryAddScoped<IEmployeeStudyProgramService, EmployeeStudyProgramService>();

        return services;
    }
}
