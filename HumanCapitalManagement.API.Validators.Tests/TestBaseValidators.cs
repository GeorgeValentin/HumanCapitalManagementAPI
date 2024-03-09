namespace HumanCapitalManagement.API.Validators.Tests;
public class TestBaseValidators
{
	protected readonly Fixture fixture;
	protected DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder;
    protected ApplicationDbContext context;

    public TestBaseValidators()
	{
		fixture = new Fixture();

        optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("TestDb");

        context = new ApplicationDbContext(optionsBuilder.Options);

        fixture.Customizations.Add(new EmployeeObjectSpecimenBuilder());
        fixture.Customizations.Add(new EmployeeSkillObjectSpecimenBuilder());

        AddDataToEmployeesInMemoryDbTable();
        AddDataToEmployeeSkillsInMemoryDbTable();
    }

    private void AddDataToEmployeesInMemoryDbTable()
    {
        var employee1 = fixture.Create<Employee>();
        employee1.PhoneNumber = "0712345678";


        context.Employees.AddRange(
            employee1,
            fixture.Create<Employee>()
        );
    }

    private void AddDataToEmployeeSkillsInMemoryDbTable()
    {
        var employeeSkill = new EmployeeSkill
        {
            Employee = null,
            EmployeeId = 1,
            Skill = null,
            SkillID = 3
        };
        var employeeSkill2 = new EmployeeSkill
        {
            Employee = null,
            EmployeeId = 1,
            Skill = null,
            SkillID = 2
        };

        context.EmployeeSkills.AddRange(
            employeeSkill,
            employeeSkill2
        );
    }
}
