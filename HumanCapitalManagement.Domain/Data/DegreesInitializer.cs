using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Domain.Data;
public static class DegreesInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (!context.Degrees.Any())
        {
            context.Degrees.AddRange(
                new Degree
                {
                    Description = "Bachelor's"
                },
                new Degree
                {
                    Description = "Master's"
                },
                new Degree
                {
                    Description = "Phd"
                },
                new Degree
                {
                    Description = "Post-Doctorate"
                }
            );

            context.SaveChanges();
        }
    }
}
