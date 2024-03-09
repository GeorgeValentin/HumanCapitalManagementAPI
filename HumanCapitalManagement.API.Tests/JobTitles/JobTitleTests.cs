namespace HumanCapitalManagement.API.Tests.JobTitles;

public class JobTitleTests
{
    private readonly Fixture fixture;
    private readonly Mock<IJobTitleService> jobTitleServiceMock;
    private readonly JobTitlesController sut;

    public JobTitleTests()
    {
        fixture = new Fixture();
        jobTitleServiceMock = new Mock<IJobTitleService>();
        sut = new JobTitlesController(jobTitleServiceMock.Object);
    }

    [Fact]
    public async void GetJobTitles_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedJobTitles = fixture.CreateMany<JobTitleDto>(3);

        jobTitleServiceMock.Setup(a => a.GetJobTitles().Result)
            .Returns(expectedJobTitles.ToList());

        // act
        var actionResponse = await sut.GetJobTitles();
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

    [Fact]
    public async void GetJobTitle_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedJobTitle = fixture.Create<JobTitleDto>();

        jobTitleServiceMock.Setup(s => s.GetJobTitle(It.IsAny<int>()).Result)
            .Returns(expectedJobTitle);

        // act
        var actionResponse = await sut.GetJobTitle(It.IsAny<int>());
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

    [Fact]
    public async void GetJobTitle_ReturnExpectedData_WhenRequestIsInvalid()
    {
        // arrange
        jobTitleServiceMock.Setup(s => s.GetJobTitle(It.IsAny<int>()).Result)
            .Returns((JobTitleDto?)null);

        // act
        var actionResponse = await sut.GetJobTitle(It.IsAny<int>());
        var response = (NotFoundResult)actionResponse.Result!;

        // assert
        Assert.IsType<NotFoundResult>(response);
        Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
    }

    [Fact]
    public async void CreateJobTitle_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var jobTitleDtoInput = fixture.Create<JobTitleForCreationDto>();
        var jobTitleDtoOutput = fixture.Create<JobTitleDto>();

        jobTitleServiceMock.Setup(s => s.CreateJobTitle(jobTitleDtoInput).Result)
            .Returns(jobTitleDtoOutput);

        // act
        var actionResponse = await sut.CreateJobTitle(jobTitleDtoInput);
        var response = (CreatedAtRouteResult)actionResponse.Result!;

        // assert
        Assert.IsType<CreatedAtRouteResult>(response);
        Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
    }

    [Fact]
    public async void UpdateJobTitle_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var jobTitleDtoInput = fixture.Create<JobTitleForUpdateDto>();

        jobTitleServiceMock.Setup(s => s.UpdateJobTitle(It.IsAny<int>(), jobTitleDtoInput))
            .Returns(Task.CompletedTask);

        // act
        var actionResponse = (OkResult)await sut.UpdateJobTitle(It.IsAny<int>(), jobTitleDtoInput);

        // assert
        Assert.IsType<OkResult>(actionResponse);
        Assert.Equal(StatusCodes.Status200OK, actionResponse.StatusCode);
    }

    [Fact]
    public async void DeleteJobTitle_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        jobTitleServiceMock.Setup(s => s.DeleteJobTitle(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // act
        var actionResponse = (NoContentResult)await sut.DeleteJobTitle(It.IsAny<int>());

        // assert
        Assert.IsType<NoContentResult>(actionResponse);
        Assert.Equal(StatusCodes.Status204NoContent, actionResponse.StatusCode);
    }
}