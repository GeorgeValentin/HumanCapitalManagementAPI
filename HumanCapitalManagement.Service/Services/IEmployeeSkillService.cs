using HumanCapitalManagement.Entities.DTOs.SkillDTOs;

namespace HumanCapitalManagement.Service.Services;
public interface IEmployeeSkillService
{
    Task<ICollection<SkillDto>> GetSkillsOfEmployee(int employeeId);
    Task<SkillDto?> GetSkillOfEmployee(int employeeId, int skillId);
    Task<ICollection<SkillDto>> AddSkillsToEmployee(int employeeId, SkillForCreationDto skillsForCreationDto);
    Task DeleteAllSkillsOfEmployeees(int employeeId);
    Task DeleteOneSkillOfEmployee(int employeeId, int skillId);
}
