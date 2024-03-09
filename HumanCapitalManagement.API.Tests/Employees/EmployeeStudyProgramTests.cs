namespace HumanCapitalManagement.API.Tests.Employee;

public class EmployeeStudyProgramTests : EmployeeBaseTests
{

    [Fact]
    public async void GetStudyProgramForEmployee_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedStudyProgram = fixture.Create<StudyProgramDto>();

        employeeStudyProgramServiceMock.Setup(s => s.GetStudyProgramForEmployee(It.IsAny<int>()).Result)
            .Returns(expectedStudyProgram);

        // act
        var actionResponse = await sut.GetStudyProgramForEmployee(It.IsAny<int>());
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

    [Fact]
    public async void GetStudyProgramForEmployee_ReturnExpectedData_WhenRequestIsInvalid()
    {
        // arrange
        employeeStudyProgramServiceMock.Setup(s => s.GetStudyProgramForEmployee(It.IsAny<int>()).Result)
            .Returns((StudyProgramDto?)null);

        // act
        var actionResponse = await sut.GetStudyProgramForEmployee(It.IsAny<int>());
        var response = (NotFoundResult)actionResponse.Result!;

        // assert
        Assert.IsType<NotFoundResult>(response);
        Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
    }

    [Fact]
    public async void AddStudyProgramToEmployee_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedStudyProgramForEmployee = fixture.Create<EmployeeDto>();

        employeeStudyProgramServiceMock.Setup(s => s.AddStudyProgramToEmployee(It.IsAny<int>(), It.IsAny<int>()).Result)
            .Returns(expectedStudyProgramForEmployee);

        EmployeeStudyProgramForCreationDto employeeStudyProgramForCreationDto = fixture.Create<EmployeeStudyProgramForCreationDto>();

        // act
        var actionResponse = await sut.AddStudyProgramToEmployee(It.IsAny<int>(), employeeStudyProgramForCreationDto);
        var response = (CreatedAtRouteResult)actionResponse.Result!;

        // assert
        Assert.IsType<CreatedAtRouteResult>(response);
        Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
    }

}
