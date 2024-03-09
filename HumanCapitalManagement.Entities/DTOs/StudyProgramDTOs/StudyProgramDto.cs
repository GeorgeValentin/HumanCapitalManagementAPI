namespace HumanCapitalManagement.Entities.DTOs.StudyProgramDTOs;

public class StudyProgramDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DegreeId { get; set; }
    public int FacultyId { get; set; }
}