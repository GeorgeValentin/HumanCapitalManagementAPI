using FluentValidation;
using HumanCapitalManagement.Entities.DTOs.ContractDTOs;

namespace HumanCapitalManagement.API.Validators.ContractValidators;

public class ContractExistanceValidator : AbstractValidator<ContractExistanceValidatorDto>
{
	public ContractExistanceValidator()
	{
        RuleFor(elem => elem.Contract)
            .NotEmpty()
            .WithMessage("The specified contract does not exist!");
    }
}
