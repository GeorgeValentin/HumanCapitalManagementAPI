namespace HumanCapitalManagement.API.Tests.Insitutions;

public class StudyProgramTests : InstitutionBaseTests
{
    [Fact]
    public async void GetStudyPrograms_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedStudyPrograms = fixture.CreateMany<StudyProgramDto>(3);

        institutionServiceMock.Setup(a => a.GetStudyPrograms(It.IsAny<int>(), It.IsAny<int>()).Result)
            .Returns(expectedStudyPrograms.ToList());

        // act
        var actionResponse = await sut.GetStudyPrograms(It.IsAny<int>(), It.IsAny<int>());
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

    [Fact]
    public async void GetStudyProgram_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedStudyProgram = fixture.Create<StudyProgramDto>();

        institutionServiceMock.Setup(s => s.GetStudyProgram(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()).Result)
            .Returns(expectedStudyProgram);

        // act
        var actionResponse = await sut.GetStudyProgram(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

    [Fact]
    public async void GetStudyProgram_ReturnExpectedData_WhenRequestIsInvalid()
    {
        // arrange
        institutionServiceMock.Setup(s => s.GetStudyProgram(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()).Result)
            .Returns((StudyProgramDto?)null);

        // act
        var actionResponse = await sut.GetStudyProgram(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());
        var response = (NotFoundResult)actionResponse.Result!;

        // assert
        Assert.IsType<NotFoundResult>(response);
        Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
    }

    [Fact]
    public async void CreatetStudyProgram_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var studyProgramDtoInput = fixture.Create<StudyProgramForCreationDto>();
        var studyProgramDtoOutput = fixture.Create<StudyProgramDto>();

        institutionServiceMock.Setup(s => s.AddStudyProgram(studyProgramDtoInput, It.IsAny<int>(), It.IsAny<int>()).Result)
            .Returns(studyProgramDtoOutput);

        // act
        var actionResponse = await sut.CreateStudyProgram(studyProgramDtoInput, It.IsAny<int>(), It.IsAny<int>());
        var response = (CreatedAtRouteResult)actionResponse.Result!;

        // assert
        Assert.IsType<CreatedAtRouteResult>(response);
        Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
    }
}
