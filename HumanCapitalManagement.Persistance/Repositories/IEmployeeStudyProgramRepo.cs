using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Persistance.Repositories
{
    public interface IEmployeeStudyProgramRepo
    {
        Task<EmployeeStudyProgram?> GetEmployeeStudyProgram(int employeeId);
        Task AddEmployeeStudyProgram(EmployeeStudyProgram employeeStudyPrograms);
        void DeleteEmployeeStudyProgram(EmployeeStudyProgram employeeStudyPrograms);
        void DeleteEmployeeStudyPrograms(ICollection<EmployeeStudyProgram> employeeStudyPrograms);
    }
}
