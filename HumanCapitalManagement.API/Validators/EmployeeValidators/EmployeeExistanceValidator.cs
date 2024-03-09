using FluentValidation;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;

namespace HumanCapitalManagement.API.Validators.EmployeeValidators;

public class EmployeeExistanceValidator : AbstractValidator<EmployeeExistanceValidatorDto>
{
    public EmployeeExistanceValidator()
    {
        RuleFor(elem => elem.Employee)
            .NotEmpty()
            .WithMessage(elem => $"This employee does not exist: {elem.EmployeeId}")
            .DependentRules(() =>
            {
                RuleFor(elem => elem.Employee)
                    .Must(elem => !elem!.IsDeleted)
                    .WithMessage(elem => $"This employee has been deleted: {elem.EmployeeId}");
            });
    }
}
