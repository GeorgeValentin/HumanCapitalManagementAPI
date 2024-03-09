using HumanCapitalManagement.Entities.DTOs.NationalityDTOs;
using HumanCapitalManagement.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace HumanCapitalManagement.API.Controllers;

[Route("api/[controller]")]
public class NationalitiesController : BaseApiController
{
    private readonly INationalitiesService _nationalitiesService;

    public NationalitiesController(
        INationalitiesService nationalitiesService)
	{
        _nationalitiesService = nationalitiesService ?? throw new ArgumentNullException(nameof(nationalitiesService));
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<NationalityDto>>> GetNationalities()
    {
        ICollection<NationalityDto> nationalities = await _nationalitiesService.GetNationalities();

        return Ok(nationalities);
    }

    [HttpGet("{nationalityId}", Name = "GetNationality")]
    public async Task<ActionResult<NationalityDto>> GetNationality([FromRoute] int nationalityId)
    {
        NationalityDto nationality = await _nationalitiesService.GetNationality(nationalityId);

        return Ok(nationality);
    }
}
