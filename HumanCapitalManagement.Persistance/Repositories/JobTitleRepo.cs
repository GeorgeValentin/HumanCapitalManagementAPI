using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Utilities.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace HumanCapitalManagement.Persistance.Repositories;
public class JobTitleRepo : IJobTitleRepo
{
    private readonly ApplicationDbContext _context;

    public JobTitleRepo(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ICollection<JobTitle>> GetJobTitles()
    {
        ICollection<JobTitle> jobTitles = await _context.JobTitles
            .AsNoTracking()
            .ToListAsync();

        Log.Information("[{class}.{method}] has been called, retrieving a number of {count} entities.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), jobTitles.Count);

        return jobTitles;
    }

    public async Task<JobTitle?> GetJobTitle(int jobTitleId)
    {
        JobTitle? jobTitle = await _context.JobTitles
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == jobTitleId);

        Log.Information("[{class}.{method}] has been called, retrieving a jobTitle from the context with the following details: {jobTitle}.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), JsonConvert.SerializeObject(jobTitle));

        return jobTitle;
    }

    public async Task AddJobTitle(JobTitle jobTitle)
    {
        Log.Information("[{class}.{method}] has been called, adding a job title to the context.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        await _context.JobTitles.AddAsync(jobTitle);
    }

    public void UpdateJobTitle(JobTitle jobTitle)
    {
        Log.Information("[{class}.{method}] has been called, updating the job title in the context.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        _context.JobTitles.Update(jobTitle);
    }

    public void RemoveJobTitle(JobTitle jobTitle)
    {
        Log.Information("[{class}.{method}] has been called, deleting the job title from the context.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        _context.JobTitles.Remove(jobTitle);
    }

}
