using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Entities.DTOs.PaginationDTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HumanCapitalManagement.Persistance.Repositories;

public interface IEmployeeRepo
{
    Task<ICollection<Employee>> GetEmployees();
    Task<Employee?> GetEmployee(int employeeId);
    Task AddEmployee(Employee employee);
    void UpdateEmployee(Employee employee);
    void DeleteEmployee(Employee employeeToDelete, JsonPatchDocument<EmployeeForCreationDto> patchDocument);
}
