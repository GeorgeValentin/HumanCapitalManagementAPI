using HumanCapitalManagement.Entities.DTOs.JobTitleDTOs;

namespace HumanCapitalManagement.Service.Services;
public interface IJobTitleService
{
    Task<JobTitleDto> CreateJobTitle(JobTitleForCreationDto jobTitleForCreationDto);
    Task DeleteJobTitle(int jobTitleId);
    Task<JobTitleDto?> GetJobTitle(int jobTitleId);
    Task<ICollection<JobTitleDto>> GetJobTitles();
    Task UpdateJobTitle(int jobTitleId, JobTitleForUpdateDto jobTitleForUpdateDto);
}
