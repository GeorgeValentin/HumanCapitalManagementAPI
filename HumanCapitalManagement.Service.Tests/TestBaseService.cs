namespace HumanCapitalManagement.Service.Tests;

public class TestBaseService
{
    protected readonly Fixture fixture;
    protected readonly Mock<IEntitiesRepo> entitiesRepoMock;

    public TestBaseService()
	{
		fixture = new Fixture();

        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        fixture.Register(() => new JsonPatchDocument<EmployeeForCreationDto>());

        entitiesRepoMock = new Mock<IEntitiesRepo>();
    }

    protected void AddSpecimenBuilder(IEnumerable<ISpecimenBuilder> specimenBuilderList)
    {
        foreach (var specimenBuilder in specimenBuilderList)
            fixture.Customizations.Add(specimenBuilder);
    }
}
