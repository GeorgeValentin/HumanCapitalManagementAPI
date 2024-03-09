using HumanCapitalManagement.Service.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Entities.DTOs.StudyProgramDTOs;
using HumanCapitalManagement.Entities.DTOs.ContractDTOs;
using HumanCapitalManagement.Entities.DTOs.SkillDTOs;
using Serilog;
using HumanCapitalManagement.Utilities.Logging;
using HumanCapitalManagement.Entities.DTOs.PaginationDTOs;
using HumanCapitalManagement.Service.Pagination;
using Newtonsoft.Json;

namespace HumanCapitalManagement.API.Controllers;

[Route("api/[controller]")]
public class EmployeesController : BaseApiController
{
    private readonly IEmployeeService _employeeService;
    private readonly IEmployeeSkillService _employeeSkillService;
    private readonly IContractService _contractService;
    private readonly IEmployeeStudyProgramService _employeeStudyProgramService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmployeesController(
        IEmployeeService employeeService,
        IEmployeeSkillService employeeSkillService,
        IContractService contractService,
        IEmployeeStudyProgramService employeeStudyProgramService,
        IHttpContextAccessor httpContextAccessor)
    {
        _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        _employeeStudyProgramService = employeeStudyProgramService;
        _employeeSkillService = employeeSkillService ?? throw new ArgumentNullException(nameof(employeeSkillService));
        _contractService = contractService ?? throw new ArgumentNullException(nameof(contractService));
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<EmployeeDto>>> GetEmployees([FromQuery] PaginationParameters? paginationParameters = null)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<EmployeeDto>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/employees",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());
        Log.Information(logMessage);

        PagedList<EmployeeDto> employeesDto = (PagedList<EmployeeDto>)await _employeeService.GetEmployees(paginationParameters);

        _httpContextAccessor.HttpContext!.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(employeesDto.MetaData));

        return Ok(employeesDto);
    }

    [HttpGet("{employeeId}", Name = "GetEmployee")]
    public async Task<ActionResult<EmployeeDto>> GetEmployee(
        [FromRoute] int employeeId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<EmployeeDto>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/employees/{employeeId}",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());
        Log.Information(logMessage, employeeId);

        EmployeeDto employeeDto = await _employeeService.GetEmployee(employeeId);

        return Ok(employeeDto);
    }

    [HttpGet("{employeeId}/skills", Name = "GetAllSkillsForEmployee")]
    public async Task<ActionResult<ICollection<SkillDto>>> GetAllSkillsForEmployee(
        [FromRoute] int employeeId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<SkillDto>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/employees/{employeeId}/skills",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());
        Log.Information(logMessage, employeeId);

        ICollection<SkillDto> employeesDto = await _employeeSkillService.GetSkillsOfEmployee(employeeId);

        return Ok(employeesDto);
    }

    [HttpGet("{employeeId}/skills/{skillId}")]
    public async Task<ActionResult<SkillDto>> GetSkillForEmployee(
        [FromRoute] int employeeId,
        [FromRoute] int skillId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<SkillDto>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/employees/{employeeId}/skills/{skillId}",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());
        Log.Information(logMessage, employeeId, skillId);

        SkillDto? skillDto = await _employeeSkillService.GetSkillOfEmployee(employeeId, skillId);

        if (skillDto == null)
            return NotFound();

        return Ok(skillDto);
    }

    [HttpGet("{employeeId}/studyprograms", Name = "GetStudyProgramForEmployee")]
    public async Task<ActionResult<StudyProgramDto>> GetStudyProgramForEmployee(
        [FromRoute] int employeeId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<StudyProgramDto>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/employees/{employeeId}/studyprograms",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());
        Log.Information(logMessage, employeeId);

        StudyProgramDto? studyProgramToReturn = await _employeeStudyProgramService.GetStudyProgramForEmployee(employeeId);

        if (studyProgramToReturn == null)
            return NotFound();
        
        return Ok(studyProgramToReturn);
    }

    [HttpGet("{employeeId}/contracts", Name = "GetContractsOfEmployee")]
    public async Task<ActionResult<ICollection<ContractDto>>> GetContractsForEmployee(
        [FromRoute] int employeeId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<ContractDto>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/employees/{employeeId}/contracts",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());
        Log.Information(logMessage, employeeId);

        ICollection<ContractDto> employeeContractsToReturn = await _contractService.GetEmployeeContracts(employeeId);

        return Ok(employeeContractsToReturn);
    }

    [HttpPost("{employeeId}/studyprograms")]
    public async Task<ActionResult<EmployeeDto>> AddStudyProgramToEmployee(
        [FromRoute] int employeeId,
        [FromBody] EmployeeStudyProgramForCreationDto employeeStudyProgramForCreationDto)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<EmployeeDto>(
            httpVerb: HttpOperationType.POST,
            endpoint: "api/employees/{employeeId}/studyprograms/{studyProgramId}",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());
        Log.Information(logMessage, employeeId, employeeStudyProgramForCreationDto.StudyProgramId);

        EmployeeDto employeeToReturn = await _employeeStudyProgramService.AddStudyProgramToEmployee(employeeId, employeeStudyProgramForCreationDto.StudyProgramId);

        return CreatedAtRoute("GetEmployee",
            new { employeeId = employeeToReturn.Id },
                employeeToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> CreateEmployee(
        [FromBody] EmployeeForCreationDto employeeForCreationDto)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<EmployeeForCreationDto>(
            httpVerb: HttpOperationType.POST,
            endpoint: "api/employees",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName(),
            entityObject: employeeForCreationDto);
        Log.Information(logMessage);

        EmployeeDto employeeToReturn = await _employeeService.AddEmployee(employeeForCreationDto);

        return CreatedAtRoute("GetEmployee",
            new { employeeId = employeeToReturn.Id },
                employeeToReturn);
    }

    [HttpPost("{employeeId}/skills")]
    public async Task<ActionResult<SkillDto>> AddSkillToEmployee(
        [FromRoute] int employeeId,
        [FromBody] SkillForCreationDto skillsForCreationDto)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<SkillForCreationDto>(
            httpVerb: HttpOperationType.POST,
            endpoint: "api/employees/{employeeId}/skills",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName(),
            entityObject: skillsForCreationDto);
        Log.Information(logMessage, employeeId);

        ICollection<SkillDto> skillsToReturn = await _employeeSkillService.AddSkillsToEmployee(employeeId, skillsForCreationDto);

        return CreatedAtRoute("GetAllSkillsForEmployee",
            new { employeeId },
                skillsToReturn);
    }

    [HttpPost("{employeeId}/contracts")]
    public async Task<ActionResult<ContractDto>> AddContractToEmployee(
        [FromRoute] int employeeId,
        [FromBody] ContractForCreationDto contractForCreationDto)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<ContractForCreationDto>(
            httpVerb: HttpOperationType.POST,
            endpoint: "api/employees/{employeeId}/contracts",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName(),
            entityObject: contractForCreationDto);

        Log.Information(logMessage, employeeId);

        ContractDto contractToReturn = await _contractService.AddContract(employeeId, contractForCreationDto);

        return CreatedAtRoute("GetContractsOfEmployee",
            new { employeeId },
                contractToReturn);
    }

    [HttpPut("{employeeId}")]
    public async Task<ActionResult> UpdateEmployee(
        [FromRoute] int employeeId,
        [FromBody] EmployeeForUpdateDto employeeForUpdateDto)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<EmployeeForUpdateDto>(
            httpVerb: HttpOperationType.PUT,
            endpoint: "api/employees/{employeeId}",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName(),
            entityObject: employeeForUpdateDto);
        Log.Information(logMessage, employeeId);
        
        await _employeeService.UpdateEmployee(employeeId, employeeForUpdateDto);

        return Ok();
    }

    [HttpPut("{employeeId}/contracts/{contractId}")]
    public async Task<ActionResult> UpdateContractOfEmployee(
        [FromRoute] int employeeId,
        [FromRoute] int contractId,
        [FromBody] ContractForUpdateDto contractForUpdateDto)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<ContractForUpdateDto>(
            httpVerb: HttpOperationType.PUT,
            endpoint: "api/employees/{employeeId}/contracts/{contractId}",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName(),
            entityObject: contractForUpdateDto);
        Log.Information(logMessage, employeeId, contractId);

        await _contractService.UpdateContract(employeeId, contractId, contractForUpdateDto);

        return Ok();
    }

    [HttpPatch("{employeeId}")]
    public async Task<ActionResult> DeleteEmployee(
        [FromRoute] int employeeId,
        JsonPatchDocument<EmployeeForCreationDto> patchDocument)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<EmployeeForCreationDto>(
            httpVerb: HttpOperationType.DELETE,
            endpoint: "api/employees/{employeeId}",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());
        Log.Information(logMessage, employeeId);

        await _employeeService.DeleteEmployee(employeeId, patchDocument);

        return NoContent();
    }

    [HttpDelete("{employeeId}/skills")]
    public async Task<ActionResult> DeleteAllSkillsOfEmployees([FromRoute] int employeeId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<SkillDto>(
            httpVerb: HttpOperationType.DELETE,
            endpoint: "api/employees/{employeeId}/skills",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());
        Log.Information(logMessage, employeeId);

        await _employeeSkillService.DeleteAllSkillsOfEmployeees(employeeId);

        return NoContent();
    }

    [HttpDelete("{employeeId}/skills/{skillToDeleteId}")]
    public async Task<ActionResult> DeleteOneSkillOfEmployee(
        [FromRoute] int employeeId,
        [FromRoute] int skillToDeleteId)
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<SkillDto>(
            httpVerb: HttpOperationType.DELETE,
            endpoint: "api/employees/{employeeId}/skills/{skillToDeleteId}",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());
        Log.Information(logMessage, employeeId, skillToDeleteId);

        await _employeeSkillService.DeleteOneSkillOfEmployee(employeeId, skillToDeleteId);

        return NoContent();
    }
}
