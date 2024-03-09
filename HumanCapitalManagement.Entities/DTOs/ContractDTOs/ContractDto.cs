using System.ComponentModel.DataAnnotations;

namespace HumanCapitalManagement.Entities.DTOs.ContractDTOs;
public class ContractDto
{
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

    public int JobTitleId { get; set; }
    public int EmployeeId { get; set; }

}
