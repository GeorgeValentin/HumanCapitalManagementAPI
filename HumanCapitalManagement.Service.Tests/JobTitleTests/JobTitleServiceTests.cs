namespace HumanCapitalManagement.Service.Tests.JobTitleTests;

public class JobTitleServiceTests : TestBaseService
{
    private readonly IMapper mapper;
    private readonly Mock<IJobTitleRepo> jobTitleRepoMock;
    private readonly Mock<IValidator<JobTitleForCreationValidatorDto>> createJobTitleValidatorMock;
    private readonly Mock<IValidator<JobTitleForUpdateValidatorDto>> updateJobTitleValidatorMock;
    private readonly Mock<IValidator<JobTitleExistanceValidatorDto>> jobTitleExistanceValidatorMock;
    private JobTitleService sut;

    public JobTitleServiceTests()
    { 
        mapper = MapperConfig<JobTitlesProfile>.ConfigureMapper();
        jobTitleRepoMock = new Mock<IJobTitleRepo>();
        createJobTitleValidatorMock = new Mock<IValidator<JobTitleForCreationValidatorDto>>();
        updateJobTitleValidatorMock = new Mock<IValidator<JobTitleForUpdateValidatorDto>>();
        jobTitleExistanceValidatorMock = new Mock<IValidator<JobTitleExistanceValidatorDto>>();
        mapper = MapperConfig<JobTitlesProfile>.ConfigureMapper();

        sut = new JobTitleService(
            jobTitleRepoMock.Object,
            mapper,
            entitiesRepoMock.Object,
            createJobTitleValidatorMock.Object,
            updateJobTitleValidatorMock.Object,
            jobTitleExistanceValidatorMock.Object);
    }

    [Fact]
    public async void GetJobTitles_ReturnExpectedData_WhenDataExists() 
    {
        // arrange
        var dbResult = fixture.Build<JobTitle>()
            .With(a => a.Id, It.IsAny<int>())
            .CreateMany(3);

        jobTitleRepoMock
            .Setup(a => a.GetJobTitles().Result)
            .Returns(dbResult.ToList());

        var expectedResult = mapper.Map<ICollection<JobTitleDto>>(dbResult);

        // act
        var result = await sut.GetJobTitles();

        // assert
        Assert.NotNull(result);
        expectedResult.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void GetJobTitle_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var dbResult = fixture.Create<JobTitle>();

        jobTitleRepoMock
            .Setup(a => a.GetJobTitle(It.IsAny<int>()).Result)
            .Returns(dbResult);

        var expectedResult = mapper.Map<JobTitleDto>(dbResult);

        // act
        var result = await sut.GetJobTitle(It.IsAny<int>());

        // assert
        Assert.NotNull(result);
        expectedResult.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void CreateJobTitle_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var creationDto = fixture.Build<JobTitleForCreationDto>()
            .Create();

        var dbInput = fixture.Create<JobTitle>();

        jobTitleRepoMock
            .Setup(a => a.AddJobTitle(dbInput))
            .Returns(Task.CompletedTask);

        var expectedResult = mapper.Map<JobTitleDto>(creationDto);

        // act
        var result = await sut.CreateJobTitle(creationDto);

        // assert
        Assert.NotNull(result);
        expectedResult.Should().BeEquivalentTo(result);
    }

    [Fact]
    public async void UpdateJobTitle_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var updateDto = fixture.Build<JobTitleForUpdateDto>()
            .Create();

        var dbModel = fixture.Create<JobTitle>();

        jobTitleRepoMock
            .Setup(a => a.GetJobTitle(It.IsAny<int>()).Result)
            .Returns(dbModel);

        Task? expectedResult = null;

        jobTitleRepoMock
            .Setup(a => a.UpdateJobTitle(dbModel))
            .Callback(() =>
            {
                expectedResult = Task.CompletedTask;
            });

        // act
        await sut.UpdateJobTitle(It.IsAny<int>(), updateDto);

        // assert
        Assert.True(expectedResult?.IsCompleted);
    }

    [Fact]
    public async void DeleteTitle_ReturnExpectedData_WhenDataExists()
    {
        // arrange
        var dbResult = fixture.Create<JobTitle>();

        jobTitleRepoMock
            .Setup(a => a.GetJobTitle(It.IsAny<int>()).Result)
            .Returns(dbResult);

        Task? expectedResult = null;
        jobTitleRepoMock
            .Setup(a => a.RemoveJobTitle(dbResult))
            .Callback(() => {
                expectedResult = Task.CompletedTask;
                dbResult = null;
            });

        // act
        await sut.DeleteJobTitle(It.IsAny<int>());

        // assert
        Assert.Null(dbResult);
        Assert.True(expectedResult?.IsCompleted);
    }
}