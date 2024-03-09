using HumanCapitalManagement.Service.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumanCapitalManagement.API.DependencyInjection.Authentication;

public static class AuthenticationServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        services.TryAddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
