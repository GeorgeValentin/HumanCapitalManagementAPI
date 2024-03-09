using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Persistance.Repositories
{
    public interface IInstitutionRepo
    {
        Task<ICollection<Institution>> GetInstitutions();
        Task<ICollection<Faculty>> GetFaculties(Institution institution);
        Task<ICollection<StudyProgram>> GetStudyPrograms(Institution institution, Faculty faculty);
        Task<Institution?> GetInstitution(int id);
        Task<Faculty?> GetFaculty(int facultyId, Institution institution);
        Task AddInstitution(Institution institution);
        Task AddFaculty(Faculty faculty);
        Task<StudyProgram?> GetStudyProgram(int studyProgramId);
        Task<StudyProgram?> GetStudyProgram(int studyProgramId, Institution institution, Faculty faculty);
        Task<StudyProgram?> GetStudyProgramForEmployee(int employeeId);
        Task AddStudyProgram(StudyProgram studyProgram);
    }
}
