using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Persistance.Repositories;
public interface IEmployeeSkillRepo
{
    Task<EmployeeSkill?> GetEmployeeSkill(int employeeId, int skillId);
    Task<ICollection<EmployeeSkill>> GetEmployeeSkills(int employeeId);
    Task AddEmployeeSkills(ICollection<EmployeeSkill> employeeSkill);
    void DeleteEmployeeSkill(EmployeeSkill employeeSkill);
    void DeleteEmployeeSkills(ICollection<EmployeeSkill> employeeSkill);
}
