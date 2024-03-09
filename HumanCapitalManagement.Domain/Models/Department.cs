using System.ComponentModel.DataAnnotations;

namespace HumanCapitalManagement.Domain.Models;
public class Department
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
