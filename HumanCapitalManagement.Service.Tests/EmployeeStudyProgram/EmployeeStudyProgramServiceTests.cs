using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanCapitalManagement.Service.Tests.EmployeeStudyProgram
{
    public class EmployeeStudyProgramServiceTests : TestBaseService
    {
        private readonly Mock<IInstitutionRepo> institutionRepoMock;
        private readonly Mock<IEmployeeStudyProgramRepo> employeeStudyProgramRepoMock;
        private readonly IMapper mapper;
        private readonly Mock<IEmployeeRepo> employeeRepoMock;
        private readonly Mock<IValidator<CreateStudyProgramForEmployeeValidatorDto>> createStudyProgramForEmployeeValidatorMock;
        private readonly Mock<IValidator<EmployeeExistanceValidatorDto>> employeeExistanceValidatorMock;
        private readonly Mock<IValidator<StudyProgramExistanceValidatorDto>> studyProgramExistanceValidatorMock;
        private EmployeeStudyProgramService sut;

        public EmployeeStudyProgramServiceTests()
        {
            mapper = MapperConfig<InstitutionProfile>.ConfigureMapper();
            employeeRepoMock = new Mock<IEmployeeRepo>();
            institutionRepoMock = new Mock<IInstitutionRepo>();
            employeeStudyProgramRepoMock = new Mock<IEmployeeStudyProgramRepo>();
            createStudyProgramForEmployeeValidatorMock = new Mock<IValidator<CreateStudyProgramForEmployeeValidatorDto>>();
            employeeExistanceValidatorMock = new Mock<IValidator<EmployeeExistanceValidatorDto>>();
            studyProgramExistanceValidatorMock = new Mock<IValidator<StudyProgramExistanceValidatorDto>>();

            sut = new EmployeeStudyProgramService(
                institutionRepoMock.Object,
                employeeStudyProgramRepoMock.Object,
                entitiesRepoMock.Object,
                mapper,
                employeeRepoMock.Object,
                createStudyProgramForEmployeeValidatorMock.Object,
                employeeExistanceValidatorMock.Object,
                studyProgramExistanceValidatorMock.Object);
        }

        [Fact]
        public async void GetStudyProgramForEmployee_ReturnExpectedData_WhenDataExists()
        {
            // arrange
            var employeeResult = fixture.Build<Employee>()
                .With(a => a.Id, It.IsAny<int>())
                .Create();
            var employeeStudyProgramResult = fixture.Build<StudyProgram>()
                .Without(a => a.Employees)
                .Create();

            employeeRepoMock
                .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
                .Returns(employeeResult);

            institutionRepoMock
                .Setup(a => a.GetStudyProgramForEmployee(It.IsAny<int>()).Result)
                .Returns(employeeStudyProgramResult);

            // act
            var result = await sut.GetStudyProgramForEmployee(It.IsAny<int>());
            var expectedResult = mapper.Map<StudyProgramDto>(employeeStudyProgramResult);

            // assert
            Assert.NotNull(result);
            expectedResult.Should().BeEquivalentTo(result);
        }

        [Fact]
        public async void AddStudyProgramToEmployee_ReturnExpectedData_WhenInputExists()
        {
            // arrange
            var employeeResult = fixture.Build<Employee>()
                .With(a => a.Id, It.IsAny<int>())
                .Create();

            var employeeStudyProgram = fixture
                .Create<Domain.Models.EmployeeStudyProgram>();

            var employeeStudyProgramResult = fixture.Build<StudyProgram>()
                .Without(a => a.Employees)
                .Create();

            var expectedResult = mapper.Map<EmployeeDto>(employeeResult);

            employeeRepoMock
                .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
                .Returns(employeeResult);

            institutionRepoMock
                .Setup(a => a.GetStudyProgram(It.IsAny<int>()).Result)
                .Returns(employeeStudyProgramResult);

            employeeStudyProgramRepoMock
                .Setup(a => a.AddEmployeeStudyProgram(employeeStudyProgram))
                .Returns(Task.CompletedTask);

            // act
            var result = await sut.AddStudyProgramToEmployee(employeeStudyProgram.Id, employeeStudyProgram.Id);

            // assert
            Assert.NotNull(result);
            expectedResult.Should().BeEquivalentTo(result);
        }
    }
}
