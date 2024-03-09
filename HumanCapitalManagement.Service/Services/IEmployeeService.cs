using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Entities.DTOs.PaginationDTOs;
using HumanCapitalManagement.Service.Pagination;
using Microsoft.AspNetCore.JsonPatch;

namespace HumanCapitalManagement.Service.Services;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetEmployees(PaginationParameters? productParameters = null);
    Task<EmployeeDto> GetEmployee(int employeeId);
    Task<EmployeeDto> AddEmployee(EmployeeForCreationDto employeeForCreationDto);
    Task UpdateEmployee(int employeeId, EmployeeForUpdateDto employeeForUpdateDto);
    Task DeleteEmployee(int employeeId, JsonPatchDocument<EmployeeForCreationDto> patchDocument);
}
