using System.ComponentModel.DataAnnotations;

namespace HumanCapitalManagement.Domain.Models;
public class JobTitle
{
    [Key]
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
}
