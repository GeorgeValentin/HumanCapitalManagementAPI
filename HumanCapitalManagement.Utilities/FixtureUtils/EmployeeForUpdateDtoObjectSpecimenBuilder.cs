using AutoFixture.Kernel;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;

namespace HumanCapitalManagement.Utilities.FixtureUtils;
public class EmployeeForUpdateDtoObjectSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(EmployeeForUpdateDto))
        {
            return new EmployeeForUpdateDto
            {
                FirstName = "Updated FName",
                LastName = "Update LName",
                Email = "updated@test.com",
                PhoneNumber = "0729110456",
                SocialSecurityNumber = "428910293124",
                NationalityId = 13,
                DepartmentId = 1
            };
        }

        return new NoSpecimen();
    }
}
