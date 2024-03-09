using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanCapitalManagement.Domain.Models;
public class EmployeeSkill
{
    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    [ForeignKey("Skill")]
    public int SkillID { get; set; }
    public Skill? Skill { get; set; }
}
