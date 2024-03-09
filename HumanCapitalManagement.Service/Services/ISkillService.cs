using HumanCapitalManagement.Entities.DTOs.SkillDTOs;

namespace HumanCapitalManagement.Service.Services;

public interface ISkillService
{
    Task<ICollection<SkillDto>> GetSkills();
    Task<SkillDto?> GetSkill(int skillId);
    Task<SkillDto> CreateSkill(SkillForCreationDto skillForCreationDto);
    Task UpdateSkill(int skillId, SkillForUpdateDto skillForUpdateDto);
    Task DeleteSkill(int skillId);
}
