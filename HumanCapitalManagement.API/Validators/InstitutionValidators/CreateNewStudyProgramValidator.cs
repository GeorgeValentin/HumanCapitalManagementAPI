using FluentValidation;
using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.StudyProgramDTOs;
using System.Text.RegularExpressions;

namespace HumanCapitalManagement.API.Validators.InstitutionValidators;

public class CreateNewStudyProgramValidator : AbstractValidator<CreateStudyProgramValidatorDto>
{
    private readonly ApplicationDbContext _context;

    public CreateNewStudyProgramValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(a => a.StudyProgramForCreationDto.DegreeId)
            .Must(elem => _context.Degrees.Any(i => i.Id == elem))
            .WithMessage("The specified degree does not exist!")
            .DependentRules(() =>
            {

                RuleFor(a => a.StudyProgramForCreationDto.Name)
                    .NotEmpty()
                    .WithMessage("The {Name} of the study program cannot be empty!")
                    .DependentRules(() =>
                    {

                        RuleFor(a => a.StudyProgramForCreationDto.Name)
                            .Length(ConstantValues.INSTITUTION_RELATED_NAME_LOWER_LENGTH, ConstantValues.INSTITUTION_RELATED_NAME_UPPER_LENGTH)
                            .WithMessage(elem => $"The {{Name}} of the study program must be between " +
                                $"{ConstantValues.INSTITUTION_RELATED_NAME_LOWER_LENGTH} and {ConstantValues.INSTITUTION_RELATED_NAME_UPPER_LENGTH}" +
                                $" characters. You entered {elem.StudyProgramForCreationDto.Name.Length}" +
                                $" characters!");

                        RuleFor(p => p.StudyProgramForCreationDto.Name)
                            .Must(a => a.Substring(0, 1).All(Char.IsUpper))
                            .When(a => a.StudyProgramForCreationDto.Name.Length > 1)
                            .WithMessage("The {Name} of the study program must start with Capital letter!");

                        RuleFor(p => p.StudyProgramForCreationDto.Name)
                            .Must(a => Regex.Match(a, @"^[a-zA-Z ]+$").Success)
                            .WithMessage("The {Name} of the study program must only contain letters and spaces!");

                    });
            });

    }
}
