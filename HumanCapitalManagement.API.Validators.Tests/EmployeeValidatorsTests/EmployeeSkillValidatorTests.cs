namespace HumanCapitalManagement.API.Validators.Tests.EmployeeValidatorsTests;

public class EmployeeSkillValidatorTests : TestBaseValidators
{
    private readonly CreateNewEmployeeSkillsValidator createNewEmployeeSkillsValidators;

    public EmployeeSkillValidatorTests()
    {
        createNewEmployeeSkillsValidators = new CreateNewEmployeeSkillsValidator(context);
    }

    [Fact]
    public void ValidateCreateEmployeeSkill_ThrowValidationTestException_WhenEmployeeSkillIsEmpty()
    {
        // arrange
        var employeeSkillForCreationValidatorDto = new EmployeeSkillForCreationValidatorDto();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            createNewEmployeeSkillsValidators.TestValidate(employeeSkillForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a));
    }

    [Fact]
    public void ValidateCreateEmployeeSkill_ThrowValidationTestException_WhenEmployeeSkillIsNotUnique()
    {
        // arrange
        var employeeSkill1 = fixture.Create<EmployeeSkill>();
        var employeeSkill2 = fixture.Create<EmployeeSkill>();

        employeeSkill1.SkillID = 2;
        employeeSkill1.EmployeeId = 1;

        var listOfEmplSkills = new List<EmployeeSkill>() { employeeSkill1, employeeSkill2 };

        var employeeSkillForCreationValidatorDto = fixture.Build<EmployeeSkillForCreationValidatorDto>()
            .With(a => a.EmployeeSkills, listOfEmplSkills)
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            createNewEmployeeSkillsValidators.TestValidate(employeeSkillForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a));
    }

}
