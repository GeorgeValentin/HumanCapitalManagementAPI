using HumanCapitalManagement.Persistance.Repositories;
using HumanCapitalManagement.Service.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumanCapitalManagement.API.DependencyInjection.Institution
{
    public static class InstitutionServiceCollectionExtensions
    {
        public static IServiceCollection AddInstitutionServices(this IServiceCollection services)
        {
            services.TryAddScoped<IInstitutionRepo, InstitutionRepo>();
            services.TryAddScoped<IInstitutionService, InstitutionService>();

            return services;
        }
    }
}
