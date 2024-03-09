using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Domain.Data;
public static class NationalitiesInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (!context.Nationalities.Any())
        {
            context.Nationalities.AddRange(
                new Nationality
                {
                    NationalityName = "Romanian"
                },
                new Nationality
                {
                    NationalityName = "French"
                },
                new Nationality
                {
                    NationalityName = "Spanish"
                },
                new Nationality
                {
                    NationalityName = "Hungarian"
                },
                new Nationality
                {
                    NationalityName = "American"
                },
                new Nationality
                {
                    NationalityName = "British"
                }
            );

            context.SaveChanges();
        }
    }
}
