using FluentValidation;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;

namespace HumanCapitalManagement.API.Validators.EmployeeValidators;

public class DeleteEmployeeSkillsValidator : AbstractValidator<EmployeeSkillsToDeleteValidatorDto>
{
	public DeleteEmployeeSkillsValidator()
	{
		RuleFor(elem => elem.EmployeeSkills)
			.NotEmpty()
			.WithMessage("The skills you are trying to delete are not assigned to this employee!");
	}
}
