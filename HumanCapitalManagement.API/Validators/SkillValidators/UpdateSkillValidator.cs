using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.SkillDTOs;

namespace HumanCapitalManagement.API.Validators.SkillValidators;

public class UpdateSkillValidator : SkillBaseValidator<SkillForUpdateValidatorDto>
{
    public UpdateSkillValidator(ApplicationDbContext context) 
        :base(context)
    { }
}
