using HumanCapitalManagement.Entities.DTOs.SkillDTOs;
using HumanCapitalManagement.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using HumanCapitalManagement.Utilities.Logging;

namespace HumanCapitalManagement.API.Controllers;


[Route("api/[controller]")]
public class SkillsController : BaseApiController
{
    private readonly ISkillService _skillService;


    public SkillsController(
        ISkillService skillService)
    {
        _skillService = skillService ?? throw new ArgumentNullException(nameof(skillService));
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<SkillDto>>> GetSkills()
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<SkillDto>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/skills",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());

        Log.Information(logMessage);

        ICollection<SkillDto> skills = await _skillService.GetSkills();

        return Ok(skills);
    }

    [HttpGet("{skillId}", Name = "GetSkill")]
    public async Task<ActionResult<SkillDto>> GetSkill([FromRoute] int skillId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<SkillDto>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/skills/{skillId}",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());

        Log.Information(logMessage, skillId);

        SkillDto? skillDto = await _skillService.GetSkill(skillId);

        if (skillDto == null)
            return NotFound();

        return Ok(skillDto);
    }

    [HttpPost]
    public async Task<ActionResult<SkillDto>> CreateSkill([FromBody] SkillForCreationDto skillForCreationDto)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<SkillForCreationDto>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/skills",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName(),
            entityObject: skillForCreationDto);

        Log.Information(logMessage);

        SkillDto skillToReturn = await _skillService.CreateSkill(skillForCreationDto);

        return CreatedAtRoute("GetSkill",
            new { skillId = skillToReturn.Id },
                skillToReturn);
    }

    [HttpPut("{skillId}")]
    public async Task<ActionResult> UpdateSkill(
        [FromRoute] int skillId,
        [FromBody] SkillForUpdateDto skillForUpdateDto)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<SkillForUpdateDto>(
            httpVerb: HttpOperationType.PUT,
            endpoint: "api/skills/{skillId}",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName(),
            entityObject: skillForUpdateDto);

        Log.Information(logMessage);

        await _skillService.UpdateSkill(skillId, skillForUpdateDto);

        return Ok();
    }

    [HttpDelete("{skillId}")]
    public async Task<ActionResult> DeleteSkill([FromRoute] int skillId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<SkillForCreationDto>(
            httpVerb: HttpOperationType.DELETE,
            endpoint: "api/skills/{skillId}",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());

        Log.Information(logMessage);

        await _skillService.DeleteSkill(skillId);

        return NoContent();
    }
}
