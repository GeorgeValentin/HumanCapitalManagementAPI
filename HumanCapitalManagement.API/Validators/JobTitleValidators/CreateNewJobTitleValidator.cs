using HumanCapitalManagement.API.Validators.JobTitleValidators;
using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.JobTitleDTOs;

namespace HumanCapitalManagement.API.Validators.JobTitleValidations;

public class CreateNewJobTitleValidator : JobTitleBaseValidator<JobTitleForCreationValidatorDto>
{
    public CreateNewJobTitleValidator(ApplicationDbContext context) 
        :base(context)
    { }
}
