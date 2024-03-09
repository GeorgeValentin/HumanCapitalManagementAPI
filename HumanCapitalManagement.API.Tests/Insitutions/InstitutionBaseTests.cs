namespace HumanCapitalManagement.API.Tests.Insitutions;

public class InstitutionBaseTests
{
    protected readonly Fixture fixture;
    protected readonly Mock<IInstitutionService> institutionServiceMock;
    protected readonly InstitutionsController sut;

    public InstitutionBaseTests()
    {
        fixture = new Fixture();
        institutionServiceMock = new Mock<IInstitutionService>();
        sut = new InstitutionsController(institutionServiceMock.Object);
    }
}
