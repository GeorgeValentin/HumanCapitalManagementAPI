using FluentValidation;
using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.FacultyDTOs;

namespace HumanCapitalManagement.API.Validators.InstitutionValidators;

public class FacultyExistanceValidator : AbstractValidator<FacultyExistanceValidatorDto>
{
	public FacultyExistanceValidator()
	{
		RuleFor(elem => elem.Faculty)
			.NotEmpty()
			.WithMessage("This faculty does not exist!");
	}
}
