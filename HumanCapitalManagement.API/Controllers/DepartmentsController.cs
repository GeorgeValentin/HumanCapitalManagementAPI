using HumanCapitalManagement.Entities.DTOs.DepartmentDTOs;
using HumanCapitalManagement.Entities.DTOs.NationalityDTOs;
using HumanCapitalManagement.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace HumanCapitalManagement.API.Controllers;

[Route("api/[controller]")]
public class DepartmentsController : BaseApiController
{
    private readonly IDepartmentService _departmentService;

    public DepartmentsController(IDepartmentService departmentService)
    {
        _departmentService = departmentService ?? throw new ArgumentNullException(nameof(departmentService));
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<DepartmentDto>>> GetDepartments()
    {
        ICollection<DepartmentDto> departments = await _departmentService.GetDepartments();

        return Ok(departments);
    }

    [HttpGet("{departmentId}", Name = "GetDepartment")]
    public async Task<ActionResult<DepartmentDto>> GetDepartment([FromRoute] int departmentId)
    {
        DepartmentDto? department = await _departmentService.GetDepartment(departmentId);

        if(department == null)
            return NotFound();

        return Ok(department);
    }
}
