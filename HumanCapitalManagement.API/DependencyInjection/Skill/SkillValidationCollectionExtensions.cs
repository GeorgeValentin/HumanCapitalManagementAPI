using FluentValidation;
using HumanCapitalManagement.API.Validators.SkillValidators;
using HumanCapitalManagement.Entities.DTOs.SkillDTOs;

namespace HumanCapitalManagement.API.DependencyInjection.Skill;

public static class SkillValidationCollectionExtensions
{
    public static IServiceCollection AddSkillValidations(this IServiceCollection services)
    {
        services.AddTransient<IValidator<SkillForCreationValidatorDto>, CreateNewSkillValidator>();
        services.AddTransient<IValidator<SkillForUpdateValidatorDto>, UpdateSkillValidator>();
        services.AddTransient<IValidator<SkillExistanceValidatorDto>, SkillExistanceValidator>();

        return services;
    }
}
