namespace HumanCapitalManagement.API.Validators.Tests.EmployeeValidatorsTests;

public class EmployeeSkillDeleteValidatorTests : TestBaseValidators
{

    [Fact]
    public void ValidateDeleteEmployeeSkills_ThrowValidationTestException_WhenEmployeeSkillsIsInvalid()
    {
        // arrange
        DeleteEmployeeSkillsValidator deleteEmployeeSkillsValidator = new DeleteEmployeeSkillsValidator();

        var employeeSkill1 = fixture.Create<EmployeeSkill>();
        var employeeSkill2 = fixture.Create<EmployeeSkill>();

        employeeSkill1.SkillID = 2;
        employeeSkill1.EmployeeId = 123;

        var listOfEmplSkills = new List<EmployeeSkill>() { employeeSkill1, employeeSkill2 };

        var employeeSkillForCreationValidatorDto = fixture.Build<EmployeeSkillsToDeleteValidatorDto>()
            .With(a => a.EmployeeSkills, listOfEmplSkills)
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            deleteEmployeeSkillsValidator.TestValidate(employeeSkillForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a));
    }

    [Fact]
    public void ValidateDeleteEmployeeSkill_ThrowValidationTestException_WhenEmployeeSkillIsInvalid()
    {
        // arrange
        DeleteEmployeeSkillValidator deleteEmployeeSkillValidator = new DeleteEmployeeSkillValidator();

        var employeeSkill = fixture.Create<EmployeeSkill>();

        employeeSkill.SkillID = 2;
        employeeSkill.EmployeeId = 123;


        var employeeSkillForCreationValidatorDto = fixture.Build<EmployeeSkillToDeleteValidatorDto>()
            .With(a => a.EmployeeSkill, employeeSkill)
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            deleteEmployeeSkillValidator.TestValidate(employeeSkillForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a));
    }
}
