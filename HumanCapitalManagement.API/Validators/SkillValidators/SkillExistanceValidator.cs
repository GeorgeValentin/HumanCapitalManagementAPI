using FluentValidation;
using HumanCapitalManagement.Entities.DTOs.SkillDTOs;

namespace HumanCapitalManagement.API.Validators.SkillValidators;

public class SkillExistanceValidator : AbstractValidator<SkillExistanceValidatorDto>
{
    public SkillExistanceValidator()
    {
        RuleFor(a => a.Skill)
            .NotEmpty()
            .WithMessage(elem => $"This skill does not exist: {elem}");
    }
}
