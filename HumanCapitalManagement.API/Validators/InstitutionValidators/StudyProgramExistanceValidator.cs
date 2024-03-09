using FluentValidation;
using HumanCapitalManagement.Entities.DTOs.StudyProgramDTOs;

namespace HumanCapitalManagement.API.Validators.InstitutionValidators;

public class StudyProgramExistanceValidator : AbstractValidator<StudyProgramExistanceValidatorDto>
{
	public StudyProgramExistanceValidator()
	{
		RuleFor(elem => elem.StudyProgram)
			.NotEmpty()
			.WithMessage("This {StudyProgram} does not exist!");
	}
}
