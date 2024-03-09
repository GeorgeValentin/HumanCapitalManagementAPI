using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Utilities.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace HumanCapitalManagement.Persistance.Repositories;
public class SkillRepo : ISkillRepo
{
    private readonly ApplicationDbContext _context;

    public SkillRepo(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ICollection<Skill>> GetSkillsOfEmployee(int employeeId)
    {
        var employeeSkillsList = await _context.Skills
            .AsNoTracking()
            .Where(item => item.Employees.Any(p => p.EmployeeId == employeeId))
            .ToListAsync();

        Log.Information("[{class}.{method}] has been called, returning {skillsCounter} skills from the context.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), employeeSkillsList.Count);

        return employeeSkillsList;
    }

    public async Task<Skill?> GetSkillOfEmployee(int employeeId, int skillId)
    {
        Skill? employeeSkill = await _context.Skills
            .AsNoTracking()
            .SingleOrDefaultAsync(item =>
                item.Id == skillId &&
                item.Employees.Any(p => p.EmployeeId == employeeId));

        Log.Information("[{class}.{method}] has been called, returning the skill: {employeeSkill} from the context.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), JsonConvert.SerializeObject(employeeSkill));

        return employeeSkill;
    }

    public async Task<ICollection<Skill>> GetSkills()
    {
        var skills = await _context.Skills
            .AsNoTracking()
            .ToListAsync();

        Log.Information("[{class}.{method}] has been called, returning {skillCounter} skills from the context.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), skills.Count);

        return skills;
    }

    public async Task<Skill?> GetSkill(int skillId)
    {
        Skill? skill = await _context.Skills
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == skillId);

        Log.Information("[{class}.{method}] has been called, returning the skill: {skill} from the context.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), skill);

        return skill;
    }

    public async Task AddSkill(Skill skillModel)
    {
        Log.Information("[{class}.{method}] has been called, adding a skill to the context.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        await _context.Skills.AddAsync(skillModel);
    }

    public void UpdateSkill(Skill skill)
    {
        Log.Information("[{class}.{method}] has been called, updating the skill from the context.",
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        _context.Skills.Update(skill);
    }

    public void RemoveSkill(Skill skillToRemove)
    {
        Log.Information("[{class}.{method}] has been called, deleting the skill from the context.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        _context.Skills.Remove(skillToRemove);
    }
}
