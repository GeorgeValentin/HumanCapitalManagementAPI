using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanCapitalManagement.Domain.Models;
public class Employee
{
    [Key]
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string SocialSecurityNumber { get; set; } = string.Empty;
    
    [Required]
    public DateTimeOffset InsertedDate { get; set; }
    
    [Required]
    public string InsertedUsername { get; set; } = string.Empty;
    
    public DateTimeOffset? UpdateDate { get; set; }

    public string? UpdateUsername { get; set; }
    
    public bool IsDeleted { get; set; }

    [ForeignKey("Address")]
    public int AddressId { get; set; }
    public Address? Address { get; set; }

    [ForeignKey("Nationality")]
    public int NationalityId { get; set; }
    public Nationality? Nationality { get; set; }

    [ForeignKey("Department")]
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    [ForeignKey("EmployeeStudyProgram")]
    public int EmployeeStudyProgramId { get; set; }

    public ICollection<EmployeeStudyProgram>? EmployeeStudyPrograms { get; set; }

    public ICollection<EmployeeSkill>? Skills { get; set; }

    public ICollection<Contract>? Contracts { get; set; }

}
