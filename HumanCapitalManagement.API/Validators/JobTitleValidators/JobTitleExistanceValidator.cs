using FluentValidation;
using HumanCapitalManagement.Entities.DTOs.JobTitleDTOs;

namespace HumanCapitalManagement.API.Validators.JobTitleValidations;

public class JobTitleExistanceValidator : AbstractValidator<JobTitleExistanceValidatorDto>
{
	public JobTitleExistanceValidator()
	{
		RuleFor(a => a.JobTitle)
			.NotEmpty()
			.WithMessage(elem => $"This jobTitle does not exist: {elem}");
    }
}
