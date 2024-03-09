using HumanCapitalManagement.API.Validators.JobTitleValidators;
using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.JobTitleDTOs;

namespace HumanCapitalManagement.API.Validators.JobTitleValidations;

public class UpdateJobTitleValidator : JobTitleBaseValidator<JobTitleForUpdateValidatorDto>
{
    public UpdateJobTitleValidator(ApplicationDbContext context) 
        :base(context)
    { }
}
