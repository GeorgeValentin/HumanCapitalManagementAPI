namespace HumanCapitalManagement.API.Tests.Employee;

public class EmployeeTests : EmployeeBaseTests
{
    [Fact]
    public async void GetEmployee_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedEmployee = fixture.Create<EmployeeDto>();

        employeeServiceMock.Setup(s => s.GetEmployee(It.IsAny<int>()).Result)
            .Returns(expectedEmployee);

        // act
        var actionResponse = await sut.GetEmployee(It.IsAny<int>());
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

    [Fact]
    public async void CreateEmployee_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var employeeDtoInput = fixture.Create<EmployeeForCreationDto>();
        var employeeDtoOutput = fixture.Create<EmployeeDto>();

        employeeServiceMock.Setup(s => s.AddEmployee(employeeDtoInput).Result)
            .Returns(employeeDtoOutput);

        // act
        var actionResponse = await sut.CreateEmployee(employeeDtoInput);
        var response = (CreatedAtRouteResult)actionResponse.Result!;

        // assert
        Assert.IsType<CreatedAtRouteResult>(response);
        Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
    }

    [Fact]
    public async void UpdateEmployee_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var employeeDtoInput = fixture.Create<EmployeeForUpdateDto>();

        employeeServiceMock.Setup(s => s.UpdateEmployee(It.IsAny<int>(), employeeDtoInput))
            .Returns(Task.CompletedTask);

        // act
        var actionResponse = (OkResult)await sut.UpdateEmployee(It.IsAny<int>(), employeeDtoInput);

        // assert
        Assert.IsType<OkResult>(actionResponse);
        Assert.Equal(StatusCodes.Status200OK, actionResponse.StatusCode);
    }

    [Fact]
    public async void DeleteEmployee_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        fixture.Register(() => new JsonPatchDocument<EmployeeForCreationDto>());
        var deleteEmployeeDoc = fixture.Create<JsonPatchDocument<EmployeeForCreationDto>>();

        employeeServiceMock.Setup(s => s.DeleteEmployee(It.IsAny<int>(), deleteEmployeeDoc))
            .Returns(Task.CompletedTask);

        // act
        var actionResponse = (NoContentResult)await sut.DeleteEmployee(It.IsAny<int>(), deleteEmployeeDoc);

        // assert
        Assert.IsType<NoContentResult>(actionResponse);
        Assert.Equal(StatusCodes.Status204NoContent, actionResponse.StatusCode);
    }

}