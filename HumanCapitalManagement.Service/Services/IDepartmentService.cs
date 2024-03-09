using HumanCapitalManagement.Entities.DTOs.DepartmentDTOs;

namespace HumanCapitalManagement.Service.Services;
public interface IDepartmentService
{
    Task<ICollection<DepartmentDto>> GetDepartments();
    Task<DepartmentDto> GetDepartment(int departmentId);
}
