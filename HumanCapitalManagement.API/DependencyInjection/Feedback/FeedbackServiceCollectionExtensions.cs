using HumanCapitalManagement.Persistance.Repositories;
using HumanCapitalManagement.Service.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HumanCapitalManagement.API.DependencyInjection.Feedback
{
    public static class FeedbackServiceCollectionExtensions
    {
        public static IServiceCollection AddFeedbackServices(this IServiceCollection services)
        {
            services.TryAddScoped<IFeebackRepo, FeedbackRepo>();
            services.TryAddScoped<IFeedbackService, FeedbackService>();

            return services;
        }
    }
}
