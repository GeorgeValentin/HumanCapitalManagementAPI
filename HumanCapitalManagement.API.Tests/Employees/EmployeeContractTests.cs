namespace HumanCapitalManagement.API.Tests.Employee;
public class EmployeeContractTests : EmployeeBaseTests
{
    [Fact]
    public async void GetContractsForEmployee_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedContracts = fixture.CreateMany<ContractDto>(3);

        contractServiceMock.Setup(s => s.GetEmployeeContracts(It.IsAny<int>()).Result)
            .Returns(expectedContracts.ToList());

        // act
        var actionResponse = await sut.GetContractsForEmployee(It.IsAny<int>());
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

    [Fact]
    public async void AddContractToEmployee_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var contractForCreationDto = fixture.Create<ContractForCreationDto>();
        var contractCreatedDto = fixture.Create<ContractDto>();

        contractServiceMock.Setup(s => s.AddContract(It.IsAny<int>(), contractForCreationDto).Result)
            .Returns(contractCreatedDto);

        // act
        var actionResponse = await sut.AddContractToEmployee(It.IsAny<int>(), contractForCreationDto);
        var response = (CreatedAtRouteResult)actionResponse.Result!;

        // assert
        Assert.IsType<CreatedAtRouteResult>(response);
        Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
    }

    [Fact]
    public async void UpdateContractOfEmployee_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var employeeDtoInput = fixture.Create<ContractForUpdateDto>();

        contractServiceMock.Setup(s => s.UpdateContract(It.IsAny<int>(), It.IsAny<int>(), employeeDtoInput))
            .Returns(Task.CompletedTask);

        // act
        var actionResponse = (OkResult)await sut.UpdateContractOfEmployee(It.IsAny<int>(), It.IsAny<int>(), employeeDtoInput);

        // assert
        Assert.IsType<OkResult>(actionResponse);
        Assert.Equal(StatusCodes.Status200OK, actionResponse.StatusCode);
    }
}
