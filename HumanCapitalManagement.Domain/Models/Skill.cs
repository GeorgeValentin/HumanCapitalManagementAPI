using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HumanCapitalManagement.Domain.Models;
public class Skill
{
    [Key]
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;

    public ICollection<EmployeeSkill>? Employees { get; set; }

}
