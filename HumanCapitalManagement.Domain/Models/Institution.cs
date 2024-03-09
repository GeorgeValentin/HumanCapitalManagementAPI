using System.ComponentModel.DataAnnotations;

namespace HumanCapitalManagement.Domain.Models;

public class Institution
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactDetails { get; set; } = string.Empty;
}
