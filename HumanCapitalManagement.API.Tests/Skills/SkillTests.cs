namespace HumanCapitalManagement.API.Tests.Skills;

public class SkillTests
{
    private readonly Fixture fixture;
    private readonly Mock<ISkillService> skillServiceMock;
    private readonly SkillsController sut;

    public SkillTests()
    {
        fixture = new Fixture();
        skillServiceMock = new Mock<ISkillService>();

        sut = new SkillsController(skillServiceMock.Object);
    }

    [Fact]
    public async void GetSkills_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var collectionExpected = fixture.CreateMany<SkillDto>(3);

        skillServiceMock.Setup(a => a.GetSkills().Result)
            .Returns(collectionExpected.ToList());

        // act
        var actionResponse = await sut.GetSkills();
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

    [Fact]
    public async void GetSkill_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedSkill = fixture.Create<SkillDto>();

        skillServiceMock.Setup(s => s.GetSkill(It.IsAny<int>()).Result)
            .Returns(expectedSkill);

        // act
        var actionResponse = await sut.GetSkill(It.IsAny<int>());
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

    [Fact]
    public async void GetSkill_ReturnExpectedData_WhenRequestIsInvalid()
    {
        // arrange
        skillServiceMock.Setup(s => s.GetSkill(It.IsAny<int>()).Result)
            .Returns((SkillDto?)null);

        // act
        var actionResponse = await sut.GetSkill(It.IsAny<int>());
        var response = (NotFoundResult)actionResponse.Result!;

        // assert
        Assert.IsType<NotFoundResult>(response);
        Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
    }

    [Fact]
    public async void CreateJobTitle_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var skillDtoInput = fixture.Create<SkillForCreationDto>();
        var skillDtoOutput = fixture.Create<SkillDto>();

        skillServiceMock.Setup(s => s.CreateSkill(skillDtoInput).Result)
            .Returns(skillDtoOutput);

        // act
        var actionResponse = await sut.CreateSkill(skillDtoInput);
        var response = (CreatedAtRouteResult)actionResponse.Result!;

        // assert
        Assert.IsType<CreatedAtRouteResult>(response);
        Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
    }

    [Fact]
    public async void UpdateSkill_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var skillDtoInput = fixture.Create<SkillForUpdateDto>();
        var skillDtoOutput = fixture.Create<SkillDto>();

        skillServiceMock.Setup(s => s.UpdateSkill(It.IsAny<int>(), skillDtoInput))
            .Returns(Task.CompletedTask);

        var sut = new SkillsController(skillServiceMock.Object);

        // act
        var actionResponse = (OkResult)await sut.UpdateSkill(It.IsAny<int>(), skillDtoInput);

        // assert
        Assert.IsType<OkResult>(actionResponse);
        Assert.Equal(StatusCodes.Status200OK, actionResponse.StatusCode);
    }

    [Fact]
    public async void DeleteSkill_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange      
        skillServiceMock.Setup(a => a.DeleteSkill(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // act
        var actionResponse = (NoContentResult)await sut.DeleteSkill(It.IsAny<int>());

        // assert
        Assert.IsType<NoContentResult>(actionResponse);
        Assert.Equal(StatusCodes.Status204NoContent, actionResponse.StatusCode);
    }
}
