using HumanCapitalManagement.Entities.DTOs.JobTitleDTOs;
using HumanCapitalManagement.Service.Services;
using HumanCapitalManagement.Utilities.Logging;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HumanCapitalManagement.API.Controllers;

[Route("api/[controller]")]
public class JobTitlesController : BaseApiController
{
    private readonly IJobTitleService _jobTitleService;

    public JobTitlesController(
        IJobTitleService jobTitleService)

    {
        _jobTitleService = jobTitleService ?? throw new ArgumentNullException(nameof(jobTitleService));
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<JobTitleDto>>> GetJobTitles()
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<JobTitleDto>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/jobtitles",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());

        Log.Information(logMessage);

        ICollection<JobTitleDto> jobTitles = await _jobTitleService.GetJobTitles();

        return Ok(jobTitles);
    }

    [HttpGet("{jobTitleId}", Name = "GetJobTitle")]
    public async Task<ActionResult<JobTitleDto>> GetJobTitle([FromRoute] int jobTitleId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<JobTitleDto>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/jobtitles/{jobTitleId}",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());

        Log.Information(logMessage, jobTitleId);

        JobTitleDto? jobTitleDto = await _jobTitleService.GetJobTitle(jobTitleId);

        if (jobTitleDto == null)
            return NotFound();

        return Ok(jobTitleDto);
    }

    [HttpPost]
    public async Task<ActionResult<JobTitleDto>> CreateJobTitle(
        [FromBody] JobTitleForCreationDto jobTitleForCreationDto)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<JobTitleForCreationDto>(
            httpVerb: HttpOperationType.POST,
            endpoint: "api/jobtitles",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName(),
            entityObject: jobTitleForCreationDto);

        Log.Information(logMessage);

        JobTitleDto jobTitleToReturn = await _jobTitleService.CreateJobTitle(jobTitleForCreationDto);

        return CreatedAtRoute("GetJobTitle",
            new { jobTitleId = jobTitleToReturn.Id },
                jobTitleToReturn);
    }

    [HttpPut("{jobTitleId}")]
    public async Task<ActionResult> UpdateJobTitle(
        [FromRoute] int jobTitleId,
        [FromBody] JobTitleForUpdateDto jobTitleForUpdateDto)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<JobTitleForUpdateDto>(
            httpVerb: HttpOperationType.PUT,
            endpoint: "api/jobtitles/{jobTitleId}",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName(),
            entityObject: jobTitleForUpdateDto);

        Log.Information(logMessage, jobTitleId);

        await _jobTitleService.UpdateJobTitle(jobTitleId, jobTitleForUpdateDto);

        return Ok();
    }

    [HttpDelete("{jobTitleId}")]
    public async Task<ActionResult> DeleteJobTitle ([FromRoute] int jobTitleId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<JobTitleForUpdateDto>(
            httpVerb: HttpOperationType.DELETE,
            endpoint: "api/jobtitles/{jobTitleId}",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());

        Log.Information(logMessage, jobTitleId);

        await _jobTitleService.DeleteJobTitle(jobTitleId);

        return NoContent();
    }

}
