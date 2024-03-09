using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Persistance.Repositories;
public interface ISkillRepo
{
    Task<ICollection<Skill>> GetSkillsOfEmployee(int employeeId);
    Task<Skill?> GetSkillOfEmployee(int employeeId, int skillId);
    Task<ICollection<Skill>> GetSkills();
    Task<Skill?> GetSkill(int skillId);
    Task AddSkill(Skill skillModel);
    void UpdateSkill(Skill skill);
    void RemoveSkill(Skill skillToRemove);
}
