using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
public class EmployeeExistanceValidatorDto
{
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
