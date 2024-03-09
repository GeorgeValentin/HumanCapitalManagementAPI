using AutoFixture.Kernel;
using HumanCapitalManagement.Domain.Models;
using Moq;

namespace HumanCapitalManagement.Utilities.FixtureUtils;
public class ContractObjectSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(Contract))
        {
            return new Contract
            {
                Id = 34,
                JobTitle = It.IsAny<JobTitle>(),
                StartDate = It.IsAny<DateTimeOffset>(),
                EndDate = It.IsAny<DateTimeOffset>()
            };
        }

        return new NoSpecimen();
    }
}
