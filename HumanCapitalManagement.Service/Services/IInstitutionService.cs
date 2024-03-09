using HumanCapitalManagement.Entities.DTOs.FacultyDTOs;
using HumanCapitalManagement.Entities.DTOs.InstitutionDTOs;
using HumanCapitalManagement.Entities.DTOs.StudyProgramDTOs;

namespace HumanCapitalManagement.Service.Services
{
    public interface IInstitutionService
    {
        Task<ICollection<InstitutionDto>> GetInstitutions();
        Task<InstitutionDto?> GetInstitution(int institutionId);
        Task<ICollection<FacultyDto>> GetFaculties(int institutionId);
        Task<ICollection<StudyProgramDto>> GetStudyPrograms(int institutionId, int facultyId);
        Task<InstitutionDto> AddInstitution(InstitutionForCreationDto institutionForCreationDto);
        Task<FacultyDto?> GetFaculty(int institutionId, int facultyId);
        Task<FacultyDto> AddFaculty(FacultyForCreationDto facultyForCreationDto, int institutionId);
        Task<StudyProgramDto?> GetStudyProgram(int institutionId, int facultyId, int studyProgramId);
        Task<StudyProgramDto> AddStudyProgram(StudyProgramForCreationDto studyProgramForCreationDto, int institutionId, int facultyId);
    }
}
