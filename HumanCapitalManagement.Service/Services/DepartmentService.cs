using AutoMapper;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.DepartmentDTOs;
using HumanCapitalManagement.Entities.DTOs.NationalityDTOs;
using HumanCapitalManagement.Persistance.Repositories;

namespace HumanCapitalManagement.Service.Services;
public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepo _departmentsRepo;
    private readonly IMapper _mapper;

    public DepartmentService(
        IDepartmentRepo departmentsRepo,
        IMapper mapper)
    {
        _departmentsRepo = departmentsRepo ?? throw new ArgumentNullException(nameof(departmentsRepo));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ICollection<DepartmentDto>> GetDepartments()
    {
        ICollection<Department> departments = await _departmentsRepo.GetDepartments();
        var departmentsToReturn = departments
            .Select(elem => _mapper.Map<DepartmentDto>(elem))
            .ToList();

        return departmentsToReturn;
    }

    public async Task<DepartmentDto> GetDepartment(int departmentId)
    {
        Department? department = await _departmentsRepo.GetDepartment(departmentId);
        var departmentToReturn = _mapper.Map<DepartmentDto>(department);

        return departmentToReturn;
    }
}
