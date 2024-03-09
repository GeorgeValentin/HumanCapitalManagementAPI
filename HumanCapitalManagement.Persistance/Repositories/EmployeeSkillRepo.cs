using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Utilities.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace HumanCapitalManagement.Persistance.Repositories;
public class EmployeeSkillRepo : IEmployeeSkillRepo
{
    private readonly ApplicationDbContext _context;

    public EmployeeSkillRepo(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ICollection<EmployeeSkill>> GetEmployeeSkills(int employeeId)
    {
        var employeeSkills = await _context.EmployeeSkills
            .AsNoTracking()
            .Where(a => a.EmployeeId == employeeId)
            .ToListAsync(); 

        Log.Information("[{class}.{method}] has been called, returning {skillsCounter} skills from the context.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), employeeSkills.Count);

        return employeeSkills;
    }

    public async Task<EmployeeSkill?> GetEmployeeSkill(int employeeId, int skillId)
    {
        var employeeSkill = await _context.EmployeeSkills
            .AsNoTracking()
            .Include(a => a.Skill)
            .Include(a => a.Employee)
            .SingleOrDefaultAsync(a => a.EmployeeId == employeeId && a.SkillID == skillId);

        Log.Information("[{class}.{method}] has been called, returning the skill: {employeeSkill} from the context.",
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), employeeSkill.Skill.Description);

        return employeeSkill;
    }

    public async Task AddEmployeeSkills(ICollection<EmployeeSkill> employeeSkill)
    {
        Log.Information("[{class}.{method}] has been called, adding a collection of EmployeeSkills to the context.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        await _context.EmployeeSkills.AddRangeAsync(employeeSkill);
    }

    public void DeleteEmployeeSkills(ICollection<EmployeeSkill> employeeSkill)
    {
        Log.Information("[{class}.{method}] has been called, deleting a collection of EmployeeSkills from the context.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        _context.EmployeeSkills.RemoveRange(employeeSkill);
    }

    public void DeleteEmployeeSkill(EmployeeSkill employeeSkill)
    {
        Log.Information("[{class}.{method}] has been called, deleting a skill from the context.", 
                    this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        _context.EmployeeSkills.Remove(employeeSkill);
    }
}
