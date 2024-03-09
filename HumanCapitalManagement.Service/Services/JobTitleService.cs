using AutoMapper;
using FluentValidation;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.JobTitleDTOs;
using HumanCapitalManagement.Persistance.Repositories;

namespace HumanCapitalManagement.Service.Services;
public class JobTitleService : IJobTitleService
{
    private readonly IJobTitleRepo _jobTitleRepo;
    private readonly IMapper _mapper;
    private readonly IEntitiesRepo _entitiesRepo;
    public readonly IValidator<JobTitleForCreationValidatorDto> _createJobTitleValidator;
    public readonly IValidator<JobTitleForUpdateValidatorDto> _updateJobTitleValidator;
    public readonly IValidator<JobTitleExistanceValidatorDto> _jobTitleExistanceValidator;

    public JobTitleService(
        IJobTitleRepo jobTitleRepo,
        IMapper mapper,
        IEntitiesRepo entitiesRepo,
        IValidator<JobTitleForCreationValidatorDto> createJobTitleValidator,
        IValidator<JobTitleForUpdateValidatorDto> updateJobTitleValidator,
        IValidator<JobTitleExistanceValidatorDto> jobTitleExistanceValidator)
    {
        _jobTitleRepo = jobTitleRepo ?? throw new ArgumentNullException(nameof(jobTitleRepo));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _entitiesRepo = entitiesRepo ?? throw new ArgumentNullException(nameof(entitiesRepo));
        _createJobTitleValidator = createJobTitleValidator ?? throw new ArgumentNullException(nameof(createJobTitleValidator));
        _updateJobTitleValidator = updateJobTitleValidator ?? throw new ArgumentNullException(nameof(updateJobTitleValidator));
        _jobTitleExistanceValidator = jobTitleExistanceValidator ?? throw new ArgumentNullException(nameof(jobTitleExistanceValidator));
    }

    public async Task<ICollection<JobTitleDto>> GetJobTitles()
    {
        var jobTitles = await _jobTitleRepo.GetJobTitles();
        var jobTitlesToReturn = jobTitles
            .Select(elem => _mapper.Map<JobTitleDto>(elem))
            .ToList();

        return jobTitlesToReturn;
    }

    public async Task<JobTitleDto?> GetJobTitle(int jobTitleId)
    {
        JobTitle? jobTitle = await _jobTitleRepo.GetJobTitle(jobTitleId);
        JobTitleDto jobTitleDto = _mapper.Map<JobTitleDto>(jobTitle);

        return jobTitleDto;
    }

    public async Task<JobTitleDto> CreateJobTitle(JobTitleForCreationDto jobTitleForCreationDto)
    {
        await _createJobTitleValidator.ValidateAndThrowAsync(
            new JobTitleForCreationValidatorDto
            {
                Description  = jobTitleForCreationDto.Description
            });

        JobTitle jobTitleModel = _mapper.Map<JobTitle>(jobTitleForCreationDto);

        await _jobTitleRepo.AddJobTitle(jobTitleModel);
        await _entitiesRepo.SaveChanges();

        JobTitleDto jobTitleToReturn = _mapper.Map<JobTitleDto>(jobTitleModel);

        return jobTitleToReturn;
    }

    public async Task UpdateJobTitle(int jobTitleId, JobTitleForUpdateDto jobTitleForUpdateDto)
    {
        JobTitle? jobTitle = await _jobTitleRepo.GetJobTitle(jobTitleId);
        await _jobTitleExistanceValidator.ValidateAndThrowAsync(
            new JobTitleExistanceValidatorDto 
            { 
                JobTitle = jobTitle 
            });

        await _updateJobTitleValidator.ValidateAndThrowAsync(
            new JobTitleForUpdateValidatorDto
            {
                Description = jobTitleForUpdateDto.Description
            });
        
        _mapper.Map(jobTitleForUpdateDto, jobTitle);

        _jobTitleRepo.UpdateJobTitle(jobTitle!);

        await _entitiesRepo.SaveChanges();
    }

    public async Task DeleteJobTitle(int jobTitleId)
    {
        var jobTitleToDelete = await _jobTitleRepo.GetJobTitle(jobTitleId);

        await _jobTitleExistanceValidator.ValidateAndThrowAsync(
            new JobTitleExistanceValidatorDto { JobTitle = jobTitleToDelete });

        _jobTitleRepo.RemoveJobTitle(jobTitleToDelete!);

        await _entitiesRepo.SaveChanges();
    }

}
