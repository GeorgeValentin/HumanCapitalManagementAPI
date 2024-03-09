using HumanCapitalManagement.Service.Authorization.Jwt;

namespace HumanCapitalManagement.API.DependencyInjection.JwtToken;

public static class JwtServiceCollectionExtensions
{
    public static IServiceCollection AddJwtServices(this IServiceCollection services)
    {
        services.AddTransient<IJwtUtils, JwtUtils>();

        return services;
    }
}
