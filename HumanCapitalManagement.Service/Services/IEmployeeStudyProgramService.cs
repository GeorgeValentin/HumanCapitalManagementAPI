using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Entities.DTOs.StudyProgramDTOs;

namespace HumanCapitalManagement.Service.Services
{
    public interface IEmployeeStudyProgramService
    {
        Task<StudyProgramDto?> GetStudyProgramForEmployee(int employeeId);
        Task<EmployeeDto> AddStudyProgramToEmployee(int employeeId, int studyProgramId);
    }
}
