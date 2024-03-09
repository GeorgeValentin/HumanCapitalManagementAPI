using AutoFixture.Kernel;
using HumanCapitalManagement.Domain.Models;
using Moq;

namespace HumanCapitalManagement.Utilities.FixtureUtils;
public class EmployeeSkillObjectSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(EmployeeSkill))
        {
            return new EmployeeSkill
            {
                Employee = null,
                EmployeeId = It.IsAny<int>(),
                Skill = null,
                SkillID = It.IsAny<int>(),
            };
        }

        return new NoSpecimen();
    }
}
