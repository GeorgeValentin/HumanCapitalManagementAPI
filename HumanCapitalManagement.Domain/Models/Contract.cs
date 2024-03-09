using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanCapitalManagement.Domain.Models;
public class Contract
{
    [Key]
    public int Id { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public double Salary { get; set; }

    [Required]
    public DateTimeOffset InsertedDate { get; set; }
    [Required]
    public string InsertedUsername { get; set; } = string.Empty;
    
    public DateTimeOffset? UpdatedDate { get; set; }
    public string? UpdatedUsername { get; set; }

    [ForeignKey("JobTitle")]
    public int JobTitleId { get; set; }
    public JobTitle? JobTitle { get; set; }

    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
