namespace HumanCapitalManagement.API.Validators.Tests.EmployeeValidatorsTests;

public class EmployeeExistanceValidatorTests : TestBaseValidators
{
    private readonly EmployeeExistanceValidator employeeExistanceValidator;

    public EmployeeExistanceValidatorTests()
    {
        employeeExistanceValidator = new EmployeeExistanceValidator();
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenEmployeeDoesNotExist()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeExistanceValidatorDto>()
            .With(a => a.Employee, new Employee())
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeExistanceValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenEmployeeHasBeenDeleted()
    {
        // arrange
        var employee = fixture.Create<Employee>();
        employee.Id = 2;
        fixture.Inject(employee);

        var employeeForCreationValidatorDto = fixture.Build<EmployeeExistanceValidatorDto>()
            .With(a => a.Employee, employee)
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeExistanceValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a));
    }
}
