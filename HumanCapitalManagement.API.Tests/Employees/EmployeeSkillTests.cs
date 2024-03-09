namespace HumanCapitalManagement.API.Tests.Employee;

public class EmployeeSkillTests : EmployeeBaseTests
{
    [Fact]
    public async void GetAllSkillsForEmployee_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedSkillsOfEmployee = fixture.CreateMany<SkillDto>(3);

        employeeSkillServiceMock.Setup(s => s.GetSkillsOfEmployee(It.IsAny<int>()).Result)
            .Returns(expectedSkillsOfEmployee.ToList());

        // act
        var actionResponse = await sut.GetAllSkillsForEmployee(It.IsAny<int>());
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

    [Fact]
    public async void GetSkillForEmployee_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedSkill = fixture.Create<SkillDto>();

        employeeSkillServiceMock.Setup(s => s.GetSkillOfEmployee(It.IsAny<int>(), It.IsAny<int>()).Result)
            .Returns(expectedSkill);

        // act
        var actionResponse = await sut.GetSkillForEmployee(It.IsAny<int>(), It.IsAny<int>());
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

    [Fact]
    public async void GetSkillForEmployee_ReturnExpectedData_WhenRequestIsInvalid()
    {
        // arrange
        employeeSkillServiceMock.Setup(s => s.GetSkillOfEmployee(It.IsAny<int>(), It.IsAny<int>()).Result)
            .Returns((SkillDto?)null);

        // act
        var actionResponse = await sut.GetSkillForEmployee(It.IsAny<int>(), It.IsAny<int>());
        var response = (NotFoundResult)actionResponse.Result!;

        // assert
        Assert.IsType<NotFoundResult>(response);
        Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
    }

    [Fact]
    public async void AddSkillToEmployee_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var skillForCreationDtoInput = fixture.Create<SkillForCreationDto>();
        var skillDtoCollectionOutput = fixture.CreateMany<SkillDto>();

        employeeSkillServiceMock.Setup(s => s.AddSkillsToEmployee(It.IsAny<int>(), skillForCreationDtoInput).Result)
            .Returns(skillDtoCollectionOutput.ToList());

        // act
        var actionResponse = await sut.AddSkillToEmployee(It.IsAny<int>(), skillForCreationDtoInput);
        var response = (CreatedAtRouteResult)actionResponse.Result!;

        // assert
        Assert.IsType<CreatedAtRouteResult>(response);
        Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
    }

    [Fact]
    public async void DeleteOneSkillOfEmployee_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        employeeSkillServiceMock.Setup(s => s.DeleteOneSkillOfEmployee(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // act
        var actionResponse = (NoContentResult)await sut.DeleteOneSkillOfEmployee(It.IsAny<int>(), It.IsAny<int>());

        // assert
        Assert.IsType<NoContentResult>(actionResponse);
        Assert.Equal(StatusCodes.Status204NoContent, actionResponse.StatusCode);
    }

    [Fact]
    public async void DeleteAllSkillsOfEmployee_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        employeeSkillServiceMock.Setup(s => s.DeleteAllSkillsOfEmployeees(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // act
        var actionResponse = (NoContentResult)await sut.DeleteAllSkillsOfEmployees(It.IsAny<int>());

        // assert
        Assert.IsType<NoContentResult>(actionResponse);
        Assert.Equal(StatusCodes.Status204NoContent, actionResponse.StatusCode);
    }
}
