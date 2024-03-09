using HumanCapitalManagement.Utilities.FixtureUtils;
using Microsoft.Extensions.DependencyInjection;

namespace HumanCapitalManagement.Persistance.Tests;

public class TestBasePersistance
{
    protected readonly Fixture fixture;
    protected DbContextOptions<ApplicationDbContext> dbContextOptions;
    protected ApplicationDbContext context;

    public TestBasePersistance()
    {
        var serviceProvider = new ServiceCollection()
        .AddEntityFrameworkInMemoryDatabase()
        .BuildServiceProvider();

        dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TestDb")
            .UseInternalServiceProvider(serviceProvider)
            .Options;

        context = new ApplicationDbContext(dbContextOptions);

        fixture = new Fixture();

        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        fixture.Register(() => new JsonPatchDocument<EmployeeForCreationDto>());
        fixture.Customizations.Add(new EmployeeObjectSpecimenBuilder());

        AddEmployeesToDbTable();
        AddInstitutionsToDbTable();
        AddFacultiesToDbTable();
        AddStudyProgramsToDbTable();
    }

    protected void AddSpecimenBuilder(IEnumerable<ISpecimenBuilder> specimenBuilderList)
    {
        foreach (var specimenBuilder in specimenBuilderList)
            fixture.Customizations.Add(specimenBuilder);
    }

    protected void AddDataToEmployeesInMemoryDbTableAsync()
    {
        var employee1 = fixture.Create<Employee>();
        var employee2 = fixture.Create<Employee>();

        employee1.PhoneNumber = "0712345678";
        employee1.Id = 12;
        employee1.Id = 24;

        context.Employees.Add(employee1);
        context.SaveChangesAsync();
        context.Employees.Add(employee2);
        context.SaveChangesAsync();
    }

    protected void AddEmployeesToDbTable()
    {
        for(int i = 1; i < 4; i++)
        {

            var employeeToAdd = fixture.Create<Employee>();
            employeeToAdd.Address = fixture.Create<Address>();
            employeeToAdd.Nationality = fixture.Create<Nationality>();
            employeeToAdd.Department = fixture.Create<Department>();
            employeeToAdd.Id = (int)i!;

            context.Employees.Add(employeeToAdd);
        }
        context.SaveChanges();
    }

    protected void AddInstitutionsToDbTable()
    {
        for (int i = 1; i < 4; i++)
        {
            var institutionToAdd = fixture.Create<Institution>();
            institutionToAdd.Id = (int)i!;

            context.Institutions.AddAsync(institutionToAdd);
        }
        context.SaveChangesAsync();
    }

    protected void AddFacultiesToDbTable()
    {
        var institutionToAdd = context
            .Institutions
            .SingleOrDefault(a => a.Id == 1);

        for (int i = 1; i < 4; i++)
        {
            var facultyToAdd = fixture
                .Build<Faculty>()
                .With(a => a.Institution, institutionToAdd)
                .With(a => a.InstitutionId, institutionToAdd.Id)
                .With(a => a.Id, i)
                .Create();

            context.Faculties.AddAsync(facultyToAdd);
        }
        context.SaveChangesAsync();
    }

    protected void AddStudyProgramsToDbTable()
    {
        var faculty = context
            .Faculties
            .FirstOrDefault(a => a.Id == 1);

        for (int i = 1; i < 4; i++)
        {
            var studyProgramToAdd = fixture
                .Build<StudyProgram>()
                .With(a => a.Id, i)
                .With(a => a.FacultyId, faculty!.Id)
                .With(a => a.Faculty, faculty)
                .With(a => a.DegreeId, 1)
                .Without(a => a.Degree)
                .Without(a => a.Employees)
                .Create();

            context.StudyPrograms.Add(studyProgramToAdd);
        }
        context.SaveChanges();
    }
}
