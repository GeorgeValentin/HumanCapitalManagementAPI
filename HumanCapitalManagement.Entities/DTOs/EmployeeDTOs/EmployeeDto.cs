using HumanCapitalManagement.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
public class EmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string SocialSecurityNumber { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    [Required]
    public DateTimeOffset InsertedDate { get; set; }
    [Required]
    public string InsertedUsername { get; set; } = string.Empty;
    public DateTimeOffset? UpdateDate { get; set; }
    public string? UpdateUsername { get; set; }
    public Address? Address { get; set; }
    public Nationality? Nationality { get; set; }
    public Department? Department { get; set; }
}
