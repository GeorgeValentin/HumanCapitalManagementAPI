using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.ContractDTOs;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Entities.DTOs.InstitutionDTOs;
using HumanCapitalManagement.Entities.DTOs.JobTitleDTOs;
using HumanCapitalManagement.Service.Services;
using HumanCapitalManagement.Utilities.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HumanCapitalManagement.API.Controllers;

[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IContractService _contractService;
    private readonly IJobTitleService _jobTitleService;
    private readonly IInstitutionService _institutionService;


    public StatisticsController(IEmployeeService employeeService,
                                IContractService contractService,
                                IJobTitleService jobTitleService,
                                IInstitutionService institutionService)
    {
        _employeeService = employeeService;
        _contractService = contractService;
        _jobTitleService = jobTitleService;
        _institutionService = institutionService;
    }

    [HttpGet("employeesCount")]
    public async Task<ActionResult<int>> GetEmployees()
    {
        var logMessage = LoggingHelper.CreateLogMessageForController<EmployeeDto>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/employees",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());
        Log.Information(logMessage);

        ICollection<EmployeeDto> employeesDto = await _employeeService.GetEmployees();

        return Ok(employeesDto.Count);
    }

    [HttpGet("contractsCount")]
    public async Task<ActionResult<int>> GetContracts()
    {
        string logMessage = LoggingHelper.CreateLogMessageForController<Contract>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/contracts",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());
        Log.Information(logMessage);

        ICollection<ContractDto>? contracts = await _contractService.GetContracts();

        return Ok(contracts!.Count);
    }

    [HttpGet("institutionsCount")]
    public async Task<ActionResult<int>> GetInstitutions()
    {
        string logMessage = LoggingHelper.CreateLogMessageForController<Contract>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/contracts",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());
        Log.Information(logMessage);

        ICollection<InstitutionDto>? institutions = await _institutionService.GetInstitutions();

        return Ok(institutions!.Count);
    }

    [HttpGet("jobsCount")]
    public async Task<ActionResult<int>> GetJobs()
    {
        string logMessage = LoggingHelper.CreateLogMessageForController<Contract>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/contracts",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());
        Log.Information(logMessage);

        ICollection<JobTitleDto>? jobs = await _jobTitleService.GetJobTitles();

        return Ok(jobs!.Count);
    }
}
