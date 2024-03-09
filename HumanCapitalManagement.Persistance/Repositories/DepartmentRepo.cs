using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Persistance.Repositories;
public class DepartmentRepo : IDepartmentRepo
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepo(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ICollection<Department>> GetDepartments()
    {
        return await _context.Departments.ToListAsync();
    }

    public async Task<Department?> GetDepartment(int departmentId)
    {
        return await _context.Departments
            .FirstOrDefaultAsync(a => a.Id == departmentId);
    }
}
