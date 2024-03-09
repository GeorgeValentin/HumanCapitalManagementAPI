namespace HumanCapitalManagement.Entities.DTOs.StudyProgramDTOs;
public class CreateStudyProgramValidatorDto
{
    public int InstitutionId { get; set; }
    public int FacultyId { get; set; }
    public StudyProgramForCreationDto StudyProgramForCreationDto { get; set; } = new StudyProgramForCreationDto();
}
