using FluentValidation;
using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.FacultyDTOs;
using System.Text.RegularExpressions;

namespace HumanCapitalManagement.API.Validators.InstitutionValidators;

public class CreateNewFacultyValidator : AbstractValidator<CreateFacultyValidatorDto>
{
    private readonly ApplicationDbContext _context;

    public CreateNewFacultyValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(a => a.FacultyForCreationDto.Name)
            .NotEmpty()
            .WithMessage("The {Name} of the faculty cannot be empty!")
            .DependentRules(() =>
            {
                RuleFor(p => p.FacultyForCreationDto.Name)
                    .Length(ConstantValues.INSTITUTION_RELATED_NAME_LOWER_LENGTH, ConstantValues.INSTITUTION_RELATED_NAME_UPPER_LENGTH)
                    .WithMessage(elem => $"The \'Name\' of the faculty must be between " +
                        $"{ConstantValues.INSTITUTION_RELATED_NAME_LOWER_LENGTH} and {ConstantValues.INSTITUTION_RELATED_NAME_UPPER_LENGTH}" +
                        $" characters. You entered {elem.FacultyForCreationDto.Name.Length} characters!");

                RuleFor(p => p.FacultyForCreationDto.Name)
                    .Must(a => a.Substring(0, 1).All(Char.IsUpper))
                    .When(a => a.FacultyForCreationDto.Name.Length > 1)
                    .WithMessage("The {Name} of the faculty must start with Capital letter!");

                RuleFor(p => p.FacultyForCreationDto.Name)
                    .Must(a => Regex.Match(a, @"^[a-zA-Z ]+$").Success)
                    .WithMessage("The {Name} of the faculty must only contain letters and spaces!");

            });
    }
}
