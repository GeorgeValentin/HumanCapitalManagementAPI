namespace HumanCapitalManagement.API.Tests.Employee;
public class EmployeeBaseTests
{
    protected readonly Fixture fixture;
    protected readonly Mock<IEmployeeService> employeeServiceMock;
    protected readonly Mock<IEmployeeSkillService> employeeSkillServiceMock;
    protected readonly Mock<IEmployeeStudyProgramService> employeeStudyProgramServiceMock;
    protected readonly Mock<IContractService> contractServiceMock;
    protected readonly Mock<IHttpContextAccessor> httpContextServiceMock;
    protected readonly EmployeesController sut;

    public EmployeeBaseTests()
    {
        fixture = new Fixture();
        employeeServiceMock = new Mock<IEmployeeService>();
        employeeSkillServiceMock = new Mock<IEmployeeSkillService>();
        employeeStudyProgramServiceMock = new Mock<IEmployeeStudyProgramService>();
        contractServiceMock = new Mock<IContractService>();
        httpContextServiceMock = new Mock<IHttpContextAccessor>();

        sut = new EmployeesController(
            employeeServiceMock.Object,
            employeeSkillServiceMock.Object,
            contractServiceMock.Object,
            employeeStudyProgramServiceMock.Object,
            httpContextServiceMock.Object);
    }
}
