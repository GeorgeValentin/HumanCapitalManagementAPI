using HumanCapitalManagement.Persistance.Repositories;
using HumanCapitalManagement.Service.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumanCapitalManagement.API.DependencyInjection;

public static class JobTitleServiceCollectionExtensions
{
    public static IServiceCollection AddJobTitleServices(this IServiceCollection services)
    {
        services.TryAddScoped<IJobTitleRepo, JobTitleRepo>();
        services.TryAddScoped<IJobTitleService, JobTitleService>();

        return services;
    }
}
