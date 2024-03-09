using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanCapitalManagement.Domain.Models;

public class Feedback
{
    [Key]
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTimeOffset InsertedDate { get; set; }
    public string InsertedUsername { get; set; } = string.Empty;
    public DateTimeOffset? UpdateDate { get; set; }
    public string? UpdateUsername { get; set; }
    public bool IsSent { get; set; }

    [ForeignKey("FromEmployeeId")]
    public Employee? FromEmployee { get; set; }
    public int? FromEmployeeId { get; set; }

    [ForeignKey("ToEmployeeId")]
    public Employee? ToEmployee { get; set; }
    public int? ToEmployeeId { get; set; }
}
