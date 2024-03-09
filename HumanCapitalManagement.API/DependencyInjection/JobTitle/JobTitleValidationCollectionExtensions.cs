using FluentValidation;
using HumanCapitalManagement.API.Validators.JobTitleValidations;
using HumanCapitalManagement.Entities.DTOs.JobTitleDTOs;

namespace HumanCapitalManagement.API.DependencyInjection.JobTitle;

public static class JobTitleValidationCollectionExtensions
{
    public static IServiceCollection AddJobTitleValidations(this IServiceCollection services)
    {
        services.AddTransient<IValidator<JobTitleForCreationValidatorDto>, CreateNewJobTitleValidator>();
        services.AddTransient<IValidator<JobTitleForUpdateValidatorDto>, UpdateJobTitleValidator>();
        services.AddTransient<IValidator<JobTitleExistanceValidatorDto>, JobTitleExistanceValidator>();

        return services;
    }
}
