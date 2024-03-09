using AutoFixture.Kernel;
using HumanCapitalManagement.Domain.Models;
using Moq;

namespace HumanCapitalManagement.Utilities.FixtureUtils;
public class AddressObjectSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(Address))
        {
            return new Address
            {
                Id = 2,
                StreetName = "TestStreet",
                StreetNumber = "123",
                PostalCode = "0291930",
                City = "TestCity",
                Country = "TestCountry"
            };
        }

        return new NoSpecimen();
    }
}
