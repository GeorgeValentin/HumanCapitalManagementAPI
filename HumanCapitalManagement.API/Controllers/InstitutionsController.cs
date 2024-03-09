using HumanCapitalManagement.Entities.DTOs.FacultyDTOs;
using HumanCapitalManagement.Entities.DTOs.InstitutionDTOs;
using HumanCapitalManagement.Entities.DTOs.StudyProgramDTOs;
using HumanCapitalManagement.Service.Services;
using HumanCapitalManagement.Utilities.Logging;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HumanCapitalManagement.API.Controllers;

[Route("api/[controller]")]
public class InstitutionsController : BaseApiController
{
    private readonly IInstitutionService _institutionService;

    public InstitutionsController(IInstitutionService institutionService)
    {
        _institutionService = institutionService ?? throw new ArgumentNullException(nameof(institutionService));
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<InstitutionDto>>> GetInstitutions()
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<InstitutionDto>(
        httpVerb: HttpOperationType.GET,
        endpoint: "api/institutions",
        className: this.GetType().Name,
        methodName: LoggingHelper.GetActualAsyncMethodName());

        Log.Information(logMessage);

        ICollection<InstitutionDto> institutionsDtos = await _institutionService.GetInstitutions();

        return Ok(institutionsDtos);
    }

    [HttpGet("{institutionId}", Name = "GetInstitution")]
    public async Task<ActionResult<InstitutionDto>> GetInstitution([FromRoute] int institutionId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<InstitutionDto>(
        httpVerb: HttpOperationType.GET,
        endpoint: "api/institutions/{institutionId}",
        className: this.GetType().Name,
        methodName: LoggingHelper.GetActualAsyncMethodName());

        Log.Information(logMessage, institutionId);

        InstitutionDto? institutionDto = await _institutionService.GetInstitution(institutionId);

        if (institutionDto == null)
            return NotFound();

        return Ok(institutionDto);
    }

    [HttpGet("{institutionId}/faculties", Name = "GetFaculties")]
    public async Task<ActionResult<ICollection<FacultyDto>>> GetFaculties([FromRoute] int institutionId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<FacultyDto>(
        httpVerb: HttpOperationType.GET,
        endpoint: "api/institutions/{institutionId}/faculties",
        className: this.GetType().Name,
        methodName: LoggingHelper.GetActualAsyncMethodName());

        Log.Information(logMessage, institutionId);

        ICollection<FacultyDto> facultiesDto = await _institutionService.GetFaculties(institutionId);

        return Ok(facultiesDto);
    }

    [HttpGet("{institutionId}/faculties/{facultyId}", Name = "GetFaculty")]
    public async Task<ActionResult<FacultyDto>> GetFaculty(
        [FromRoute] int institutionId, 
        [FromRoute] int facultyId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<FacultyDto>(
        httpVerb: HttpOperationType.GET,
        endpoint: "api/institutions/{institutionId}/faculties/{facultyId}",
        className: this.GetType().Name,
        methodName: LoggingHelper.GetActualAsyncMethodName());

        Log.Information(logMessage, institutionId, facultyId);

        FacultyDto? facultyDto = await _institutionService.GetFaculty(institutionId, facultyId);

        if (facultyDto == null)
            return NotFound();

        return Ok(facultyDto);
    }

    [HttpGet("{institutionId}/faculties/{facultyId}/studyprograms", Name = "GetStudyPrograms")]
    public async Task<ActionResult<ICollection<StudyProgramDto>>> GetStudyPrograms(
        [FromRoute] int institutionId, 
        [FromRoute] int facultyId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<StudyProgramDto>(
        httpVerb: HttpOperationType.GET,
        endpoint: "api/institutions/{institutionId}/faculties/{facultyId}/studyprograms",
        className: this.GetType().Name,
        methodName: LoggingHelper.GetActualAsyncMethodName());

        Log.Information(logMessage, institutionId, facultyId);

        ICollection<StudyProgramDto> studyProgramDtos = await _institutionService.GetStudyPrograms(institutionId, facultyId);

        return Ok(studyProgramDtos);
    }

    [HttpGet("{institutionId}/faculties/{facultyId}/studyprograms/{studyProgramId}", Name = "GetStudyProgram")]
    public async Task<ActionResult<StudyProgramDto>> GetStudyProgram(
        [FromRoute] int institutionId, 
        [FromRoute] int facultyId, 
        [FromRoute] int studyProgramId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<StudyProgramDto>(
        httpVerb: HttpOperationType.GET,
        endpoint: "api/institutions/{institutionId}/faculties/{facultyId}/studyprograms/{studyProgramId}",
        className: this.GetType().Name,
        methodName: LoggingHelper.GetActualAsyncMethodName());

        Log.Information(logMessage, institutionId, facultyId, studyProgramId);

        StudyProgramDto? studyProgramDtos = await _institutionService
            .GetStudyProgram(institutionId, facultyId, studyProgramId);

        if (studyProgramDtos == null)
            return NotFound();

        return Ok(studyProgramDtos);
    }

    [HttpPost]
    public async Task<ActionResult<InstitutionDto>> CreateInstitution(
        [FromBody] InstitutionForCreationDto institutionForCreationDto)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<InstitutionForCreationDto>(
        httpVerb: HttpOperationType.POST,
        endpoint: "api/institutions",
        className: this.GetType().Name,
        methodName: LoggingHelper.GetActualAsyncMethodName(),
        entityObject: institutionForCreationDto);

        Log.Information(logMessage);

        InstitutionDto institutionToReturn = await _institutionService
            .AddInstitution(institutionForCreationDto);

        return CreatedAtRoute("GetInstitution",
            new { institutionId = institutionToReturn.Id },
                institutionToReturn);
    }

    [HttpPost("{institutionId}/faculties")]
    public async Task<ActionResult<FacultyDto>> CreateFaculty(
        [FromBody] FacultyForCreationDto facultyForCreationDto,
        [FromRoute] int institutionId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<FacultyForCreationDto>(
        httpVerb: HttpOperationType.POST,
        endpoint: "api/institutions/{institutionId}/faculties",
        className: this.GetType().Name,
        methodName: LoggingHelper.GetActualAsyncMethodName(),
        entityObject: facultyForCreationDto);

        Log.Information(logMessage, institutionId);

        FacultyDto facultyToReturn = await _institutionService.AddFaculty(facultyForCreationDto, institutionId);

        return CreatedAtRoute("GetFaculty",
            new { facultyId = facultyToReturn.Id, institutionId },
                facultyToReturn);
    }

    [HttpPost("{institutionId}/faculties/{facultyId}/studyprograms")]
    public async Task<ActionResult<StudyProgramDto>> CreateStudyProgram(
        [FromBody] StudyProgramForCreationDto studyProgramForCreationDto,
        [FromRoute] int institutionId,
        [FromRoute] int facultyId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<StudyProgramForCreationDto>(
        httpVerb: HttpOperationType.POST,
        endpoint: "api/institutions/{institutionId}/faculties/{facultyId}/studyprograms",
        className: this.GetType().Name,
        methodName: LoggingHelper.GetActualAsyncMethodName(),
        entityObject: studyProgramForCreationDto);

        Log.Information(logMessage, institutionId, facultyId);

        StudyProgramDto studyProgramToReturn = await _institutionService.AddStudyProgram(studyProgramForCreationDto, institutionId, facultyId);

        return CreatedAtRoute("GetStudyProgram",
            new { studyProgramId = studyProgramToReturn.Id, institutionId, facultyId },
                studyProgramToReturn);
    }
}
