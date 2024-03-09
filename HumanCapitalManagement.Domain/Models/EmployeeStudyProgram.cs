using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanCapitalManagement.Domain.Models
{
    public class EmployeeStudyProgram
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [ForeignKey("StudyProgram")]
        public int StudyProgramId { get; set; }
        public StudyProgram? StudyProgram { get; set; }
    }
}
