using FluentValidation;
using HumanCapitalManagement.API.Validators.AddressValidators;
using HumanCapitalManagement.API.Validators.EmployeeValidators;
using HumanCapitalManagement.Entities.DTOs.AddressDTOs;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;

namespace HumanCapitalManagement.API.DependencyInjection.Employee;

public static class EmployeeValidationCollectionExtensions
{
    public static IServiceCollection AddEmployeeValidations(this IServiceCollection services)
    {
        services.AddTransient<IValidator<AddressForCreationValidatorDto>, CreateNewAddressValidator>();
        services.AddTransient<IValidator<EmployeeForCreationValidatorDto>, CreateNewEmployeeValidator>();
        services.AddTransient<IValidator<EmployeeForUpdateValidatorDto>, UpdateEmployeeValidator>();
        services.AddTransient<IValidator<EmployeeExistanceValidatorDto>, EmployeeExistanceValidator>();
        services.AddTransient<IValidator<EmployeeSkillsToDeleteValidatorDto>, DeleteEmployeeSkillsValidator>();
        services.AddTransient<IValidator<EmployeeSkillToDeleteValidatorDto>, DeleteEmployeeSkillValidator>();
        services.AddTransient<IValidator<EmployeeSkillForCreationValidatorDto>, CreateNewEmployeeSkillsValidator>();

        return services;
    }
}
