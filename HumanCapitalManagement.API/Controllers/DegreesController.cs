using HumanCapitalManagement.Entities.DTOs.DegreeDTOs;
using HumanCapitalManagement.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace HumanCapitalManagement.API.Controllers;

[Route("api/[controller]")]
public class DegreesController : BaseApiController
{
	private readonly IDegreeService _degreeService;
	public DegreesController(
		IDegreeService degreeService)
	{
		_degreeService = degreeService;
	}

	[HttpGet]
	public async Task<ActionResult<ICollection<DegreeDto>>> GetDegrees()
	{
        ICollection<DegreeDto> degreesDto = await _degreeService.GetDegrees();

		return Ok(degreesDto);
	}

    [HttpGet("{degreeId}", Name = "GetDegree")]
	public async Task<ActionResult<DegreeDto>> GetDegree(int degreeId)
	{
		DegreeDto? degreeDto = await _degreeService.GetDegree(degreeId);

		if (degreeDto == null)
			return NotFound();

		return Ok(degreeDto);
	}
}
