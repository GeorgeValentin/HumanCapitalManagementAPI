using System.ComponentModel.DataAnnotations.Schema;

namespace HumanCapitalManagement.Domain.Models
{
    public class StudyProgram
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [ForeignKey("Degree")]
        public int DegreeId { get; set; }
        public Degree Degree { get; set; }

        [ForeignKey("Faculty")]
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public ICollection<EmployeeStudyProgram>? Employees { get; set; }
    }
}
