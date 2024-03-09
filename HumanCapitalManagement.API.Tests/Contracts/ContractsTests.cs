namespace HumanCapitalManagement.API.Tests.Contract;

public class ContractsTests
{
    private readonly Fixture fixture;
    private readonly Mock<IContractService> contractServiceMock;
    private readonly ContractsController sut;

    public ContractsTests()
    {
        fixture = new Fixture();
        contractServiceMock = new Mock<IContractService>();
        sut = new ContractsController(contractServiceMock.Object);
    }

    [Fact]
    public async void GetContract_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedContract = fixture.Create<ContractDto>();

        contractServiceMock.Setup(s => s.GetContract(It.IsAny<int>()).Result)
            .Returns(expectedContract);

        // act
        var actionResponse = await sut.GetContract(It.IsAny<int>());
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

}