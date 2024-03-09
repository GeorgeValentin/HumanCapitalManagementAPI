using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.AddressDTOs;

namespace HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
public class EmployeeForCreationDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string SocialSecurityNumber { get; set; } = string.Empty;
    public Address? Address { get; set; }
    public int NationalityId { get; set; }
    public int DepartmentId { get; set; }
}
