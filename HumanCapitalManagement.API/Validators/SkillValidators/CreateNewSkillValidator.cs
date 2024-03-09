using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.JobTitleDTOs;
using HumanCapitalManagement.Entities.DTOs.SkillDTOs;

namespace HumanCapitalManagement.API.Validators.SkillValidators;

public class CreateNewSkillValidator : SkillBaseValidator<SkillForCreationValidatorDto>
{
    public CreateNewSkillValidator(ApplicationDbContext context) 
        :base(context)
    { }
}
