using HumanCapitalManagement.Persistance.Repositories;
using HumanCapitalManagement.Service.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumanCapitalManagement.API.DependencyInjection.Skill;
public static class SkillServiceCollectionExtensions
{
    public static IServiceCollection AddSkillServices(this IServiceCollection services)
    {
        services.TryAddScoped<ISkillRepo, SkillRepo>();
        services.TryAddScoped<ISkillService, SkillService>();

        return services;
    }
}
