using FluentValidation;
using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.ContractDTOs;

namespace HumanCapitalManagement.API.Validators.ContractValidators;

public class UpdateContractValidator : AbstractValidator<ContractUpdateValidatorDto>
{
    private readonly ApplicationDbContext _context;

    public UpdateContractValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(a => a.JobTitleId)
            .NotEmpty()
            .WithMessage("The {JobTitleId} cannot be empty!")
            .DependentRules(() => {
                
                RuleFor(a => a.JobTitleId)
                   .Must(jobTitleId => _context.JobTitles.Any(jobTitle => jobTitle.Id == jobTitleId))
                   .WithMessage("The specified job title does not exist!");

            });
        }
}

