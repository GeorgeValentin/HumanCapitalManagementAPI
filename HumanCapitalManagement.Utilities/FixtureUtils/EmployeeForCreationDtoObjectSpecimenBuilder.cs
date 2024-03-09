using AutoFixture.Kernel;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;

namespace HumanCapitalManagement.Utilities.FixtureUtils;
public class EmployeeForCreationDtoObjectSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(EmployeeForCreationDto))
        {
            return new EmployeeForCreationDto
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Email = "someEmail@test.com",
                PhoneNumber = "0729810456",
                SocialSecurityNumber = "428910293124",
                NationalityId = 12,
                DepartmentId = 2
            };
        }

        return new NoSpecimen();
    }
}
