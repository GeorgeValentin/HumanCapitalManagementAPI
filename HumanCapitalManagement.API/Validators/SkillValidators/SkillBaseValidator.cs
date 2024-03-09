using FluentValidation;
using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.SkillDTOs;
using System.Text.RegularExpressions;

namespace HumanCapitalManagement.API.Validators.SkillValidators;

public abstract class SkillBaseValidator<T> : AbstractValidator<T> where T : SkillBaseValidatorDto
{
    private readonly ApplicationDbContext _context;

    protected SkillBaseValidator(ApplicationDbContext context)
	{
        _context = context;

        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("The {Description} cannot be empty!")
            .DependentRules(() =>
            {
                RuleFor(p => p.Description)
                    .Length(5, 20);

                RuleFor(p => p.Description)
                    .Must(a => a.Substring(0, 1).All(Char.IsUpper))
                    .When(a => a.Description.Length > 1)
                    .WithMessage("The {Description} must start with Capital letter!");

                RuleFor(p => p.Description)
                    .Must(a => Regex.Match(a, @"^[a-zA-Z ]+$").Success)
                    .WithMessage("The {Description} must only contain letters and spaces!");

                RuleFor(p => p.Description)
                    .Must(elem => !_context.Skills.Any(b => b.Description
                        .Equals(elem)))
                    .WithMessage("The {Description} must be unique!");
            });
    }
}
