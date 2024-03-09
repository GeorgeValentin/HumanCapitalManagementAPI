using System.ComponentModel.DataAnnotations.Schema;

namespace HumanCapitalManagement.Domain.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [ForeignKey("Institution")]
        public int InstitutionId { get; set; }
        public Institution? Institution { get; set; }
    }
}
