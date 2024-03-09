using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Domain.Data;
public static class DepartmentsInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (!context.Departments.Any())
        {
            context.Departments.AddRange(
                new Department
                {
                    Name = "Sales",
                    Description = "Sales Description"
                },
                new Department
                {
                    Name = "IT",
                    Description = "IT Description"
                },
                new Department
                {
                    Name = "Accounting",
                    Description = "Accounting Description"
                },
                new Department
                {
                    Name = "Finance",
                    Description = "Finance Description"
                }
            );

            context.SaveChanges();
        }
    }
}
