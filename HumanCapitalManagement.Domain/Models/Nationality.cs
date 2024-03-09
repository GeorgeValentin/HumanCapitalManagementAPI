using System.ComponentModel.DataAnnotations;

namespace HumanCapitalManagement.Domain.Models;
public class Nationality
{
    [Key]
    public int Id { get; set; }
    public string NationalityName { get; set; } = string.Empty;
}
