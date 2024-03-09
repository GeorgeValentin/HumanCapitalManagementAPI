namespace HumanCapitalManagement.API.Tests.Insitutions;

public class FacultyTests : InstitutionBaseTests
{
    [Fact]
    public async void GetFaculties_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedFaculties = fixture.CreateMany<FacultyDto>(3);

        institutionServiceMock.Setup(a => a.GetFaculties(It.IsAny<int>()).Result)
            .Returns(expectedFaculties.ToList());

        // act
        var actionResponse = await sut.GetFaculties(It.IsAny<int>());
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

    [Fact]
    public async void GetFaculty_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedFaculty = fixture.Create<FacultyDto>();

        institutionServiceMock.Setup(s => s.GetFaculty(It.IsAny<int>(), It.IsAny<int>()).Result)
            .Returns(expectedFaculty);

        // act
        var actionResponse = await sut.GetFaculty(It.IsAny<int>(), It.IsAny<int>());
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

    [Fact]
    public async void GetFaculty_ReturnExpectedData_WhenRequestIsInvalid()
    {
        // arrange
        institutionServiceMock.Setup(s => s.GetFaculty(It.IsAny<int>(), It.IsAny<int>()).Result)
            .Returns((FacultyDto?)null);

        // act
        var actionResponse = await sut.GetFaculty(It.IsAny<int>(), It.IsAny<int>());
        var response = (NotFoundResult)actionResponse.Result!;

        // assert
        Assert.IsType<NotFoundResult>(response);
        Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
    }

    [Fact]
    public async void CreatetFaculty_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var facultyDtoInput = fixture.Create<FacultyForCreationDto>();
        var facultyDtoOutput = fixture.Create<FacultyDto>();

        institutionServiceMock.Setup(s => s.AddFaculty(facultyDtoInput, It.IsAny<int>()).Result)
            .Returns(facultyDtoOutput);

        // act
        var actionResponse = await sut.CreateFaculty(facultyDtoInput, It.IsAny<int>());
        var response = (CreatedAtRouteResult)actionResponse.Result!;

        // assert
        Assert.IsType<CreatedAtRouteResult>(response);
        Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
    }
}
