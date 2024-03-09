using FluentValidation;
using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.ContractDTOs;

namespace HumanCapitalManagement.API.Validators.ContractValidators;

public class CreateContractValidator : AbstractValidator<ContractCreateValidatorDto>
{
    private readonly ApplicationDbContext _context;

    public CreateContractValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(a => a.ContractForCreationDto.JobTitleId)
            .Must(jobTitleId => _context.JobTitles.Any(jobTitle => jobTitle.Id == jobTitleId))
            .WithMessage("The specified job title does not exist!")
            .DependentRules(() =>
            {
                RuleFor(a => a.ContractForCreationDto.Salary)
                    .GreaterThanOrEqualTo(ConstantValues.SALARY_THRESHOLD)
                    .WithMessage($"Cannot add a salary below this threshold: {ConstantValues.SALARY_THRESHOLD}");

                RuleFor(a => a.ContractForCreationDto.StartDate)
                    .Must(elem => DateTimeOffset.Compare(elem.Date, DateTimeOffset.UtcNow.Date) > 0)
                    .WithMessage(elem => "The contract must have a start date later than the current day!");
            });
    }
}
