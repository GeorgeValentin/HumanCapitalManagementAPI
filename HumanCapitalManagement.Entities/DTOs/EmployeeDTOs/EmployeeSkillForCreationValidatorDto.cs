using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
public class EmployeeSkillForCreationValidatorDto
{
    public ICollection<EmployeeSkill>? EmployeeSkills { get; set; }
}
