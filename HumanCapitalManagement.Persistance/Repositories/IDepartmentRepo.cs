using HumanCapitalManagement.Domain.Models;
namespace HumanCapitalManagement.Persistance.Repositories;
public interface IDepartmentRepo
{
    Task<ICollection<Department>> GetDepartments();
    Task<Department?> GetDepartment(int departmentId);
}
