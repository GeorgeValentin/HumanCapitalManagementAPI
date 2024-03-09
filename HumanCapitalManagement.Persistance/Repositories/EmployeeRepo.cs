using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Entities.DTOs.PaginationDTOs;
using HumanCapitalManagement.Utilities.Logging;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace HumanCapitalManagement.Persistance.Repositories;

public class EmployeeRepo : IEmployeeRepo
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepo(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ICollection<Employee>> GetEmployees()
    {
        ICollection<Employee>?  employees = await _context.Employees
                .AsNoTracking()
                .Include(a => a.Address)
                .Include(proj => proj.Nationality)
                .Include(a => a.Department)
                .Where(empl => !empl.IsDeleted)
                .OrderBy(a => a.Id)
                .ToListAsync();

        Log.Information("[{class}.{method}] has been called, retrieving: {employeesCount} employees.",
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), employees.Count);

        return employees;
    }

    public async Task<Employee?> GetEmployee(int employeeId)
    {
        Employee? employee = await _context.Employees
            .AsNoTracking()
            .Include(proj => proj.Address)
            .Include(proj => proj.Nationality)
            .Include(a => a.Department)
            .FirstOrDefaultAsync(empl => empl.Id == employeeId);

        Log.Information("[{class}.{method}] has been called, retrieving the employee: {employee}.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), JsonConvert.SerializeObject(employee));

        return employee;
    }

    public async Task AddEmployee(Employee employee)
    {
        Log.Information("[{class}.{method}] has been called, adding an employee to the context.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        await _context.Employees.AddAsync(employee);
    }

    public void UpdateEmployee(Employee employee)
    {
        Log.Information("[{class}.{method}] has been called, updating an employee from the context.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        _context.Employees.Update(employee);
    }

    public void DeleteEmployee(Employee employeeToDelete, JsonPatchDocument<EmployeeForCreationDto> patchDocument)
    {
        Log.Information("[{class}.{method}] has been called, deleting an employee from the context.",
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        employeeToDelete!.IsDeleted = Convert.ToBoolean(patchDocument.Operations[0].value);
        UpdateEmployee(employeeToDelete);
    }

}
