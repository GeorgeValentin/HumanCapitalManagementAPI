using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Persistance.Repositories;
public interface IJobTitleRepo
{
    Task<ICollection<JobTitle>> GetJobTitles();
    Task<JobTitle?> GetJobTitle(int jobTitleId);
    Task AddJobTitle(JobTitle jobTitle);
    void UpdateJobTitle(JobTitle jobTitle);
    void RemoveJobTitle(JobTitle jobTitle);
}
