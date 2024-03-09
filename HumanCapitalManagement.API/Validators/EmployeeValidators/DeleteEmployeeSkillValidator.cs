using FluentValidation;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;

namespace HumanCapitalManagement.API.Validators.EmployeeValidators;

public class DeleteEmployeeSkillValidator : AbstractValidator<EmployeeSkillToDeleteValidatorDto>
{
	public DeleteEmployeeSkillValidator()
	{
		RuleFor(elem => elem.EmployeeSkill)
			.NotEmpty()
			.WithMessage("The skill you are trying to delete is not assigned to this employee!");
	}
}
