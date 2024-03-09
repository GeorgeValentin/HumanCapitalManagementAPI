using FluentValidation;
using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.JobTitleDTOs;
using System.Text.RegularExpressions;

namespace HumanCapitalManagement.API.Validators.JobTitleValidators;

public abstract class JobTitleBaseValidator<T> : AbstractValidator<T> where T: JobTitleBaseValidatorDto
{
	private readonly ApplicationDbContext _context;

	protected JobTitleBaseValidator(ApplicationDbContext context)
	{
		_context = context;

        RuleFor(p => p.Description)
           .NotEmpty()
           .WithMessage("The job title cannot be empty!")
           .DependentRules(() =>
           {
               RuleFor(p => p.Description)
                   .Length(5, 20);

               RuleFor(p => p.Description)
                   .Must(a => a.Substring(0, 1).All(Char.IsUpper))
                   .When(a => a.Description.Length > 1)
                   .WithMessage("The job title must start with Capital letter!");

               RuleFor(p => p.Description)
                   .Must(a => Regex.Match(a, @"^[a-zA-Z ]+$").Success)
                   .WithMessage("The job title must only contain letters and spaces!");

               RuleFor(p => p.Description)
                   .Must(elem => !_context.JobTitles.Any(b => b.Description
                       .Equals(elem)))
                   .WithMessage("The job title must be unique!");
           });
    }
}
