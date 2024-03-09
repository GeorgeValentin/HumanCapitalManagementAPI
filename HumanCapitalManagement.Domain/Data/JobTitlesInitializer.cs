using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Domain.Data;
public static class JobTitlesInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (!context.JobTitles.Any())
        {
            context.JobTitles.AddRange(
                new JobTitle
                {
                    Description = "Programmer"
                },
                new JobTitle
                {
                    Description = "IT Manager"
                },
                new JobTitle
                {
                    Description = "Sales Agent"
                },
                new JobTitle
                {
                    Description = "Accountant"
                },
                new JobTitle
                {
                    Description = "Marketing Manager"
                },
                new JobTitle
                {
                    Description = "Driver"
                }
            );

            context.SaveChanges();
        }
    }
}
