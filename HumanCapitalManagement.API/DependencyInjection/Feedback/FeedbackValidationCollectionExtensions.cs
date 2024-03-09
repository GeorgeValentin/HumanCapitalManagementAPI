using FluentValidation;
using HumanCapitalManagement.API.Validators.EmployeeValidators;
using HumanCapitalManagement.API.Validators.FeedbackValidators;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Entities.DTOs.FeedbackDTOs;

namespace HumanCapitalManagement.API.DependencyInjection.Feedback;

public static class FeedbackValidationCollectionExtensions
{
    public static IServiceCollection AddFeedbackValidations(this IServiceCollection services)
    {
        services.AddTransient<IValidator<EmployeeExistanceValidatorDto>, EmployeeExistanceValidator>();
        services.AddTransient<IValidator<FeedbackForCreationValidatorDto>, CreateNewFeedbackValidator>();

        return services;
    }
}
