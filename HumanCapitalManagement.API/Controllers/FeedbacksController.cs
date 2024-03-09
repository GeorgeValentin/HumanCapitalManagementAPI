using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Entities.DTOs.FeedbackDTOs;
using HumanCapitalManagement.Service.Services;
using HumanCapitalManagement.Utilities.Logging;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HumanCapitalManagement.API.Controllers;

[Route("api/[controller]")]
public class FeedbacksController : BaseApiController
{
	private readonly IFeedbackService _feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService ?? throw new ArgumentNullException(nameof(feedbackService));
        }

        [HttpPost]
	public async Task<ActionResult<FeedbackDto>> CreateFeedback(
	[FromBody] FeedbackForCreationDto feedbackForCreationDto)
	{
            var logMessage = LoggingHelper.CreateLogMessageForController<EmployeeDto>(
			httpVerb: HttpOperationType.POST,
			endpoint: "api/feedbacks",
			className: this.GetType().Name,
			methodName: LoggingHelper.GetActualAsyncMethodName());
            Log.Information(logMessage);

		FeedbackDto feedbackToReturn = await _feedbackService.AddFeedback(feedbackForCreationDto);

		return CreatedAtRoute("GetFeedback",
			new { feedbackId = feedbackToReturn.Id },
				feedbackToReturn);
	}

	[HttpGet("{feedbackId}", Name = "GetFeedback")]
	public async Task<ActionResult<FeedbackDto>> GetFeedback([FromRoute] int feedbackId)
	{
            var logMessage = LoggingHelper.CreateLogMessageForController<EmployeeDto>(
                httpVerb: HttpOperationType.GET,
                endpoint: "api/feedbacks/{feedbackId}",
                className: this.GetType().Name,
                methodName: LoggingHelper.GetActualAsyncMethodName());
            Log.Information(logMessage, feedbackId);

            FeedbackDto? feedbackDto = await _feedbackService.GetFeedbackById(feedbackId);

		if (feedbackDto == null)
			return NotFound();

		return Ok(feedbackDto);
	}

	[HttpGet("", Name = "GetFeedbacks")]
	public async Task<ActionResult<ICollection<FeedbackDto>>> GetFeedbacks([FromQuery] int employeeId, [FromQuery] AssessorType assessorType)
	{
            var logMessage = LoggingHelper.CreateLogMessageForController<EmployeeDto>(
                httpVerb: HttpOperationType.GET,
                endpoint: "api/feedbacks?employeeId={employeeId}&assessorType={assessorType}",
                className: this.GetType().Name,
                methodName: LoggingHelper.GetActualAsyncMethodName());
            Log.Information(logMessage, employeeId, assessorType);

            ICollection<FeedbackDto>? feedbacksDto = await _feedbackService.GetFeedbacks(employeeId, assessorType);

		return Ok(feedbacksDto);
	}
}
