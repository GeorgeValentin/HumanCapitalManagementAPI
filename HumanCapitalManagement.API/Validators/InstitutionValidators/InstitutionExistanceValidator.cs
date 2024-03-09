using FluentValidation;
using HumanCapitalManagement.Entities.DTOs.InstitutionDTOs;

namespace HumanCapitalManagement.API.Validators.InstitutionValidators;

public class InstitutionExistanceValidator : AbstractValidator<InstitutionExistanceValidatorDto>
{
	public InstitutionExistanceValidator()
	{

		RuleFor(elem => elem.Institution)
			.NotEmpty()
			.WithMessage("This institution does not exist!");
	}
}
