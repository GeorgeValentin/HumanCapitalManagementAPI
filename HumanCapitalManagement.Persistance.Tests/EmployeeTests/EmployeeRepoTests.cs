namespace HumanCapitalManagement.Persistance.Tests.EmployeeTests;
public class EmployeeRepoTests : TestBasePersistance
{
    private EmployeeRepo sut;

    public EmployeeRepoTests()
    {
        sut = new EmployeeRepo(context);
    }

    [Fact]
    public async Task GetEmployees_ReturnExpecteddata_WhenDataExists()
    {
        // act
        var result = await sut.GetEmployees();

        // assert
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async Task GetEmployee_ReturnExpecteddata_WhenDataExists()
    {
        // act
        var result = await sut.GetEmployee(2);

        // assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task AddEmployee_ReturnExpecteddata_WhenDataExists()
    {
        // act
        var initialDbCounter = (await sut.GetEmployees()).Count;

        var idToAdd = 100;

        var employeeToAdd = fixture.Create<Employee>();
        employeeToAdd.Address = fixture.Create<Address>();
        employeeToAdd.Nationality = fixture.Create<Nationality>();
        employeeToAdd.Department = fixture.Create<Department>();
        employeeToAdd.Id = (int)idToAdd!;

        await sut.AddEmployee(employeeToAdd);
        await context.SaveChangesAsync();

        var result = await sut.GetEmployees();

        // assert
        Assert.Equal(initialDbCounter + 1, result.Count);
    }

}
