using AutoFixture.Kernel;
using HumanCapitalManagement.Domain.Models;
using Moq;

namespace HumanCapitalManagement.Utilities.FixtureUtils;
public class EmployeeObjectSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(Employee))
        {
            return new Employee
            {
                Id = It.IsAny<int>(),
                AddressId = It.IsAny<int>(),
                NationalityId = It.IsAny<int>(),
                DepartmentId = It.IsAny<int>(),
                EmployeeStudyProgramId = It.IsAny<int>(),
                EmployeeStudyPrograms = null,
                Skills = null,
                Contracts = null,
                IsDeleted = false,
            };
        }

        return new NoSpecimen();
    }
}
