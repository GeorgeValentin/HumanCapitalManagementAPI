using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
public class EmployeeSkillsToDeleteValidatorDto
{
    public ICollection<EmployeeSkill>? EmployeeSkills { get; set; }
}
