namespace HumanCapitalManagement.Service.Tests.EmployeeSkillTests;

public class EmployeeSkillServiceTests : TestBaseService
{
    private readonly IMapper mapper;
    private readonly Mock<ISkillRepo> skillRepoMock;
    private readonly Mock<IEmployeeSkillRepo> employeeSkillRepoMock;
    private readonly Mock<IEmployeeRepo> employeeRepoMock;
    private readonly Mock<IValidator<EmployeeSkillsToDeleteValidatorDto>> deleteEmployeeSkillsValidatorMock;
    private readonly Mock<IValidator<EmployeeSkillToDeleteValidatorDto>> deleteEmployeeSkillValidatorMock;
    private readonly Mock<IValidator<EmployeeSkillForCreationValidatorDto>> createEmployeeSkilValidatorMock;
    private readonly Mock<IValidator<EmployeeExistanceValidatorDto>> employeeExistanceValidatorMock;
    private EmployeeSkillService sut;

    public EmployeeSkillServiceTests()
    {
        mapper = MapperConfig<SkillsProfile>.ConfigureMapper();
        employeeRepoMock = new Mock<IEmployeeRepo>();
        skillRepoMock = new Mock<ISkillRepo>();
        employeeSkillRepoMock = new Mock<IEmployeeSkillRepo>();
        deleteEmployeeSkillsValidatorMock = new Mock<IValidator<EmployeeSkillsToDeleteValidatorDto>>();
        deleteEmployeeSkillValidatorMock = new Mock<IValidator<EmployeeSkillToDeleteValidatorDto>>();
        createEmployeeSkilValidatorMock = new Mock<IValidator<EmployeeSkillForCreationValidatorDto>>();
        employeeExistanceValidatorMock = new Mock<IValidator<EmployeeExistanceValidatorDto>>();

        fixture.Customizations.Add(new EmployeeSkillObjectSpecimenBuilder());

        sut = new EmployeeSkillService(
            skillRepoMock.Object,
            mapper,
            entitiesRepoMock.Object,
            employeeSkillRepoMock.Object,
            employeeRepoMock.Object,
            deleteEmployeeSkillsValidatorMock.Object,
            deleteEmployeeSkillValidatorMock.Object,
            createEmployeeSkilValidatorMock.Object,
            employeeExistanceValidatorMock.Object);
    }

    [Fact]
    public async void GetEmployeeSkills_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var employeeResult = fixture.Build<Employee>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();
        var employeeSkillsResult = fixture.Build<Skill>()
            .Without(a => a.Employees)
            .CreateMany();
        var expectedResult = mapper.Map<ICollection<SkillDto>>(employeeSkillsResult);

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(employeeResult);

        skillRepoMock
            .Setup(a => a.GetSkillsOfEmployee(It.IsAny<int>()).Result)
            .Returns(employeeSkillsResult.ToList());

        // act
        var result = await sut.GetSkillsOfEmployee(It.IsAny<int>());

        // assert
        Assert.NotNull(result);
        expectedResult.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void GetEmployeeSkill_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var employeeResult = fixture.Build<Employee>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();
        var employeeSkillResult = fixture.Build<Skill>()
            .Without(a => a.Employees)
            .Create();
        var expectedResult = mapper.Map<SkillDto>(employeeSkillResult);

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(employeeResult);

        skillRepoMock
            .Setup(a => a.GetSkillOfEmployee(It.IsAny<int>(), It.IsAny<int>()).Result)
            .Returns(employeeSkillResult);

        // act
        var result = await sut.GetSkillOfEmployee(It.IsAny<int>(), It.IsAny<int>());

        // assert
        Assert.NotNull(result);
        expectedResult.Should().BeEquivalentTo<SkillDto>(result);
    }

    [Fact]
    public async void AddSkillsToEmployee_ReturnExpectedData_WhenInputExists()
    {
        // arrange
        var employeeResult = fixture.Build<Employee>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();
        var skillForCreationDto = fixture.Build<SkillForCreationDto>()
            .Without(a => a.Description)
            .Create();
        var employeeSkillsCollection = fixture
            .CreateMany<EmployeeSkill>()
            .ToList();
        var employeeSkillsResult = fixture.Build<Skill>()
            .Without(a => a.Employees)
            .CreateMany()
            .ToList();

        var expectedResult = mapper.Map<ICollection<SkillDto>>(employeeSkillsResult);

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(employeeResult);

        employeeSkillRepoMock
            .Setup(a => a.AddEmployeeSkills(employeeSkillsCollection))
            .Returns(Task.CompletedTask);

        skillRepoMock
            .Setup(a => a.GetSkillsOfEmployee(It.IsAny<int>()).Result)
            .Returns(employeeSkillsResult);

        // act
        var result = await sut.AddSkillsToEmployee(It.IsAny<int>(), skillForCreationDto);

        // assert
        Assert.NotNull(result);
        expectedResult.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void AddSkillsToEmployee_ReturnExpectedData_WhenInputDoesNotExistAndTheEmployeeDoesNotHaveAnySkills()
    {
        // arrange
        var employeeResult = fixture.Build<Employee>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();
        var skillForCreationDto = fixture.Build<SkillForCreationDto>()
            .Without(a => a.Description)
            .Create();
        skillForCreationDto.CollectionOfSkills = null;

        ICollection<Skill>? skillsToReturn = null;
        ICollection<SkillDto>? skillsToReturnDto = mapper.Map<ICollection<SkillDto>>(skillsToReturn);
        
        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(employeeResult);

        skillRepoMock
            .Setup(a => a.GetSkillsOfEmployee(It.IsAny<int>()).Result)
            .Returns(skillsToReturn!);

        // act
        var result = await sut.AddSkillsToEmployee(It.IsAny<int>(), skillForCreationDto);

        // assert
        Assert.NotNull(result);
        skillsToReturnDto.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void AddSkillsToEmployee_ReturnExpectedData_WhenInputDoesNotExistAndTheEmployeeHasSkills()
    {
        // arrange
        var employeeResult = fixture.Build<Employee>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();
        var skillForCreationDto = fixture.Build<SkillForCreationDto>()
            .Without(a => a.Description)
            .With(a => a.CollectionOfSkills, new List<int>())
            .Create();

        var skillsToReturn = fixture
            .CreateMany<Skill>()
            .ToList();
        var skillsToReturnDto = mapper.Map<ICollection<SkillDto>>(skillsToReturn);

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(employeeResult);

        skillRepoMock
            .Setup(a => a.GetSkillsOfEmployee(It.IsAny<int>()).Result)
            .Returns(skillsToReturn);

        // act
        var result = await sut.AddSkillsToEmployee(It.IsAny<int>(), skillForCreationDto);

        // assert
        Assert.NotNull(result);
        skillsToReturnDto.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void DeleteAllSkillsOfEmployee_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var employeeResult = fixture.Build<Employee>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();

        var employeeSkillsRelationsToDeleteResult = fixture
            .CreateMany<EmployeeSkill>()
            .ToList();

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(employeeResult);

        employeeSkillRepoMock
            .Setup(a => a.GetEmployeeSkills(It.IsAny<int>()).Result)
            .Returns(employeeSkillsRelationsToDeleteResult);

        Task? expectedResult = null;
        employeeSkillRepoMock
            .Setup(a => a.DeleteEmployeeSkills(employeeSkillsRelationsToDeleteResult))
            .Callback(() => {
                expectedResult = Task.CompletedTask;
                employeeSkillsRelationsToDeleteResult = null;
            });

        // act
        await sut.DeleteAllSkillsOfEmployeees(It.IsAny<int>());

        // assert
        Assert.Null(employeeSkillsRelationsToDeleteResult);
        Assert.True(expectedResult?.IsCompleted);
    }

    [Fact]
    public async void DeleteOneSkillOfEmployee_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var employeeResult = fixture.Build<Employee>()
            .With(a => a.Id, It.IsAny<int>())
            .Create();

        var employeeSkillRelationsToDeleteResult = fixture
            .Create<EmployeeSkill>();

        employeeRepoMock
            .Setup(a => a.GetEmployee(It.IsAny<int>()).Result)
            .Returns(employeeResult);

        employeeSkillRepoMock
            .Setup(a => a.GetEmployeeSkill(It.IsAny<int>(), It.IsAny<int>()).Result)
            .Returns(employeeSkillRelationsToDeleteResult);

        Task? expectedResult = null;
        employeeSkillRepoMock
            .Setup(a => a.DeleteEmployeeSkill(employeeSkillRelationsToDeleteResult))
            .Callback(() => {
                expectedResult = Task.CompletedTask;
                employeeSkillRelationsToDeleteResult = null;
            });

        // act
        await sut.DeleteOneSkillOfEmployee(It.IsAny<int>(), It.IsAny<int>());

        // assert
        Assert.Null(employeeSkillRelationsToDeleteResult);
        Assert.True(expectedResult?.IsCompleted);
    }
}
