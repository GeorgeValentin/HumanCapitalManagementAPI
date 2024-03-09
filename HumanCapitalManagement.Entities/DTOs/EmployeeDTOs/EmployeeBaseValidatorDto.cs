namespace HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
public class EmployeeBaseValidatorDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string SocialSecurityNumber { get; set; } = string.Empty;
}
