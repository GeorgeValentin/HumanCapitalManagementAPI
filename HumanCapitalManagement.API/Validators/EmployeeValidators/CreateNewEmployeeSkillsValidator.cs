using FluentValidation;
using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;

namespace HumanCapitalManagement.API.Validators.EmployeeValidators;

public class CreateNewEmployeeSkillsValidator : AbstractValidator<EmployeeSkillForCreationValidatorDto>
{
	private readonly ApplicationDbContext? _context;

	public CreateNewEmployeeSkillsValidator(ApplicationDbContext context)
	{
		_context = context;

		RuleFor(elem => elem.EmployeeSkills)
			.NotEmpty()
			.WithMessage("The collection of skills you are trying to add cannot be empty!")
			.DependentRules(() =>
			{
				RuleFor(elem => elem.EmployeeSkills)
				.ForEach(emplSkill =>
				{
					emplSkill.Must(elem => !_context.EmployeeSkills.Any(emplSkill => emplSkill.Equals(elem)))
						     .WithMessage("There is a duplicate in the collections of skills you are trying to add, try again!");
				});
			});
	}
}
