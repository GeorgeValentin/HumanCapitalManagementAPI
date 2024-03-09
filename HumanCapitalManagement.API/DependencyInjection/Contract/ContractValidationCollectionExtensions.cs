using FluentValidation;
using HumanCapitalManagement.API.Validators.ContractValidators;
using HumanCapitalManagement.Entities.DTOs.ContractDTOs;

namespace HumanCapitalManagement.API.DependencyInjection.Contract;

public static class ContractValidationCollectionExtensions
{
    public static IServiceCollection AddContractValidations(this IServiceCollection services)
    {
        services.AddTransient<IValidator<ContractCreateValidatorDto>, CreateContractValidator>();
        services.AddTransient<IValidator<ContractUpdateValidatorDto>, UpdateContractValidator>();
        services.AddTransient<IValidator<ContractExistanceValidatorDto>, ContractExistanceValidator>();

        return services;
    }

}
