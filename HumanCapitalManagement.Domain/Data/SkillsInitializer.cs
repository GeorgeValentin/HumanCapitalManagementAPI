using HumanCapitalManagement.Domain.Models;
namespace HumanCapitalManagement.Domain.Data;

public static class SkillsInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (!context.Skills.Any())
        {
            context.Skills.AddRange(
                new Skill
                {
                    Description = "Technical Thinking"
                },
                new Skill
                {
                    Description = "Sales"
                },
                new Skill
                {
                    Description = "Public Speaking"
                },
                new Skill
                {
                    Description = "Good Organizer"
                },
                new Skill
                {
                    Description = "Teamwork"
                },
                new Skill
                {
                    Description = "Creativity"
                }
            );

            context.SaveChanges();
        }
    }
}
