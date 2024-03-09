using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.ContractDTOs;
using HumanCapitalManagement.Service.Authorization;
using HumanCapitalManagement.Service.Services;
using HumanCapitalManagement.Utilities.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HumanCapitalManagement.API.Controllers;

[Route("api/[controller]")]
public class ContractsController : BaseApiController
{
    private readonly IContractService _contractService;

    public ContractsController(IContractService contractService)
    {
        _contractService = contractService ?? throw new ArgumentNullException(nameof(contractService));
    }

    [HttpGet("{contractId}", Name = "GetContract")]
    public async Task<ActionResult<ContractDto>> GetContract([FromRoute] int contractId)
    {
        string logMessage = LoggingHelper.CreateLogMessageForController<Contract>(
            httpVerb: HttpOperationType.GET,
            endpoint: "api/contracts/{contractId}",
            className: this.GetType().Name,
            methodName: LoggingHelper.GetActualAsyncMethodName());
        Log.Information(logMessage, contractId);

        ContractDto? contractDto = await _contractService.GetContract(contractId);

        return Ok(contractDto);
    }

}
