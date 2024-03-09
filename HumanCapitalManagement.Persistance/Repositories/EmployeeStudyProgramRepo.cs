using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Utilities.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace HumanCapitalManagement.Persistance.Repositories
{
    public class EmployeeStudyProgramRepo : IEmployeeStudyProgramRepo
    {
        private readonly ApplicationDbContext _context;

        public EmployeeStudyProgramRepo(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddEmployeeStudyProgram(EmployeeStudyProgram employeeStudyPrograms)
        {
            Log.Information("[{class}.{method}] has been called, adding an employeeStudyPrograms to the context.", 
                this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

            await _context.EmployeesStudyPrograms.AddAsync(employeeStudyPrograms);
        }

        public void DeleteEmployeeStudyProgram(EmployeeStudyProgram employeeStudyPrograms)
        {
            Log.Information("[{class}.{method}] has been called, deleting an employeeStudyPrograms from the context.", 
                this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

            _context.EmployeesStudyPrograms.Remove(employeeStudyPrograms);
        }

        public void DeleteEmployeeStudyPrograms(ICollection<EmployeeStudyProgram> employeeStudyPrograms)
        {
            Log.Information("[{class}.{method}] has been called, deleting a collection of employeeStudyPrograms from the context.", 
                this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

            _context.EmployeesStudyPrograms.RemoveRange(employeeStudyPrograms);
        }

        public async Task<EmployeeStudyProgram?> GetEmployeeStudyProgram(int employeeId)
        {
            var employeeStudyProgram = await _context.EmployeesStudyPrograms
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.EmployeeId == employeeId);

            Log.Information("[{class}.{method}] has been called, returning the {employeeStudyProgram} employeeStudyProgram from the context.",
                this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), employeeStudyProgram);

            return employeeStudyProgram;
        }
    }
}
