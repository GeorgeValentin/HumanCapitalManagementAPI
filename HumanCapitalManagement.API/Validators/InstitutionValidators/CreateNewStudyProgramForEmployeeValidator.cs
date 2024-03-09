using FluentValidation;
using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.StudyProgramDTOs;

namespace HumanCapitalManagement.API.Validators.InstitutionValidators;

public class CreateNewStudyProgramForEmployeeValidator : AbstractValidator<CreateStudyProgramForEmployeeValidatorDto>
{
    private readonly ApplicationDbContext _context;

    public CreateNewStudyProgramForEmployeeValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(elem => elem)
            .Must(elem => !_context.Employees.Where(a => a.Id == elem.EmployeeId).Any(a => a.EmployeeStudyProgramId != 0))
            .WithMessage("This employee has a study program already!");
    }
}
