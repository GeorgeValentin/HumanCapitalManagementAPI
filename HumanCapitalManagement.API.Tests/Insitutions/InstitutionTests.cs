namespace HumanCapitalManagement.API.Tests.Insitutions;

public class InstitutionTests : InstitutionBaseTests
{
    [Fact]
    public async void GetInstitutions_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedInstitutions = fixture.CreateMany<InstitutionDto>(3);

        institutionServiceMock.Setup(a => a.GetInstitutions().Result)
            .Returns(expectedInstitutions.ToList());

        // act
        var actionResponse = await sut.GetInstitutions();
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

    [Fact]
    public async void GetInstitution_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var expectedInstitution = fixture.Create<InstitutionDto>();

        institutionServiceMock.Setup(s => s.GetInstitution(It.IsAny<int>()).Result)
            .Returns(expectedInstitution);

        // act
        var actionResponse = await sut.GetInstitution(It.IsAny<int>());
        var response = (OkObjectResult)actionResponse.Result!;

        // assert
        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
    }

    [Fact]
    public async void GetInstitution_ReturnExpectedData_WhenRequestIsInvalid()
    {
        // arrange
        institutionServiceMock.Setup(s => s.GetInstitution(It.IsAny<int>()).Result)
            .Returns((InstitutionDto?)null);

        // act
        var actionResponse = await sut.GetInstitution(It.IsAny<int>());
        var response = (NotFoundResult)actionResponse.Result!;

        // assert
        Assert.IsType<NotFoundResult>(response);
        Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
    }

    [Fact]
    public async void CreateInstitution_ReturnExpectedData_WhenRequestIsValid()
    {
        // arrange
        var institutitonDtoInput = fixture.Create<InstitutionForCreationDto>();
        var institutionDtoOutput = fixture.Create<InstitutionDto>();

        institutionServiceMock.Setup(s => s.AddInstitution(institutitonDtoInput).Result)
            .Returns(institutionDtoOutput);

        // act
        var actionResponse = await sut.CreateInstitution(institutitonDtoInput);
        var response = (CreatedAtRouteResult)actionResponse.Result!;

        // assert
        Assert.IsType<CreatedAtRouteResult>(response);
        Assert.Equal(StatusCodes.Status201Created, response.StatusCode);
    }
}