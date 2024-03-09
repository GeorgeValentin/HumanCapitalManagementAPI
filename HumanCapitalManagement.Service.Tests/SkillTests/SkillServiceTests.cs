namespace HumanCapitalManagement.Service.Tests.SkillTests;

public class SkillServiceTests : TestBaseService
{
    private readonly Mock<ISkillRepo> skillRepoMock;
    private readonly Mock<IValidator<SkillForCreationValidatorDto>> createSkillValidatorMock;
    private readonly Mock<IValidator<SkillForUpdateValidatorDto>> updateSkillValidatorMock;
    private readonly Mock<IValidator<SkillExistanceValidatorDto>> skillExistanceValidatorMock;
    protected readonly IMapper mapper;
    private SkillService sut;

    public SkillServiceTests()
    {
        skillRepoMock = new Mock<ISkillRepo>();
        createSkillValidatorMock = new Mock<IValidator<SkillForCreationValidatorDto>>();
        updateSkillValidatorMock = new Mock<IValidator<SkillForUpdateValidatorDto>>();
        skillExistanceValidatorMock = new Mock<IValidator<SkillExistanceValidatorDto>>();
        mapper = MapperConfig<SkillsProfile>.ConfigureMapper();

        sut = new SkillService(
            mapper,
            skillRepoMock.Object,
            entitiesRepoMock.Object,
            createSkillValidatorMock.Object,
            skillExistanceValidatorMock.Object,
            updateSkillValidatorMock.Object);
    }

    [Fact]
    public async void GetSkills_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var dbResult = fixture.Build<Skill>()
            .With(a => a.Id, It.IsAny<int>())
            .Without(a => a.Employees)
            .CreateMany(3).ToList();

        skillRepoMock
            .Setup(a => a.GetSkills().Result)
            .Returns(dbResult);

        var expectedResult = mapper.Map<ICollection<SkillDto>>(dbResult);

        // act
        var result = await sut.GetSkills();

        // assert
        Assert.NotNull(result);
        expectedResult.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void GetSkill_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var dbResult = fixture.Build<Skill>()
            .With(a => a.Id, It.IsAny<int>())
            .Without(a => a.Employees)
            .Create();

        skillRepoMock
            .Setup(a => a.GetSkill(It.IsAny<int>()).Result)
            .Returns(dbResult);

        var expectedResult = mapper.Map<SkillDto>(dbResult);

        // act
        var result = await sut.GetSkill(It.IsAny<int>());

        // assert
        Assert.NotNull(result);
        expectedResult.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void CreateSkill_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var skillForCreationDto = fixture.Build<SkillForCreationDto>()
            .Without(a => a.CollectionOfSkills)
            .Create();

        var dbInput = fixture.Build<Skill>()
            .With(a => a.Id, It.IsAny<int>())
            .Without(a => a.Employees)
            .Create();

        skillRepoMock
            .Setup(a => a.AddSkill(dbInput))
            .Returns(Task.CompletedTask);

        var expectedResult = mapper.Map<SkillDto>(skillForCreationDto);

        // act
        var result = await sut.CreateSkill(skillForCreationDto);

        // assert
        Assert.NotNull(result);
        expectedResult.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void UpdateSkill_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var updateDto = fixture.Build<SkillForUpdateDto>()
            .Create();

        var dbModel = fixture.Build<Skill>()
            .With(a => a.Id, It.IsAny<int>())
            .Without(a => a.Employees)
            .Create();

        skillRepoMock
            .Setup(a => a.GetSkill(It.IsAny<int>()).Result)
            .Returns(dbModel);

        Task? expectedResult = null;
        skillRepoMock
            .Setup(a => a.UpdateSkill(dbModel))
            .Callback(() =>
            {
                expectedResult = Task.CompletedTask;
            });

        // act
        await sut.UpdateSkill(It.IsAny<int>(), updateDto);

        // assert
        Assert.True(expectedResult?.IsCompleted);
    }

    [Fact]
    public async void DeleteSkill_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var dbResult = fixture.Build<Skill>()
            .With(a => a.Id, It.IsAny<int>())
            .Without(a => a.Employees)
            .Create();
        skillRepoMock
            .Setup(a => a.GetSkill(It.IsAny<int>()).Result)
            .Returns(dbResult);

        Task? expectedResult = null;
        skillRepoMock
            .Setup(a => a.RemoveSkill(dbResult))
            .Callback(() => {
                expectedResult = Task.CompletedTask;
                dbResult = null;
            });

        // act
        await sut.DeleteSkill(It.IsAny<int>());

        // assert
        Assert.Null(dbResult);
        Assert.True(expectedResult?.IsCompleted);
    }
}
