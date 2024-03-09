using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HumanCapitalManagement.Domain.Data;
public static class DbInitializer
{
    public static void EnsurePopulated(IApplicationBuilder app)
    {
        ApplicationDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();

        NationalitiesInitializer.Initialize(context);
        DepartmentsInitializer.Initialize(context);
        SkillsInitializer.Initialize(context);
        JobTitlesInitializer.Initialize(context);
        DegreesInitializer.Initialize(context);
    }
}
