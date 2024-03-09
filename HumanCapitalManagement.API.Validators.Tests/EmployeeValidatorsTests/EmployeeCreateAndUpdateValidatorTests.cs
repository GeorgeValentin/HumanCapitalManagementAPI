namespace HumanCapitalManagement.API.Validators.Tests.EmployeeValidatorsTests;

public class EmployeeCreateAndUpdateValidatorTests : TestBaseValidators
{
    private EmployeeBaseValidator<EmployeeBaseValidatorDto> employeeValidator;

    public EmployeeCreateAndUpdateValidatorTests()
    {
        employeeValidator = new EmployeeBaseValidator<EmployeeBaseValidatorDto>();
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenEmployeeIsEmpty()
    {
        employeeValidator = new EmployeeBaseValidator<EmployeeBaseValidatorDto>(context);

        // arrange
        var employeeForCreationValidatorDto = new EmployeeBaseValidatorDto();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenFirstNameIsEmpty()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .Without(a => a.FirstName)
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.FirstName));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenFirstNameLengthIsInvalid()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .With(a => a.FirstName, new string('a', 101))
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.FirstName));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenFirstNameCasingIsInvalid()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .With(a => a.FirstName, "testString")
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.FirstName));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenFirstNameDoesNotContainOnlyLettersAndSpaces()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .With(a => a.FirstName, "testString2")
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.FirstName));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenLastNameIsEmpty()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .Without(a => a.LastName)
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.LastName));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenLastNameLengthIsInvalid()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .With(a => a.LastName, new string('a', 101))
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.LastName));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenLastNameCasingIsInvalid()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .With(a => a.LastName, "testString")
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.LastName));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenLastNameDoesNotContainOnlyLettersAndSpaces()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .With(a => a.LastName, "testString2")
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.LastName));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenEmailIsEmpty()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .With(a => a.Email, "")
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.FirstName));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenEmailIsNotValid()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .With(a => a.Email, "someEmailHere")
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.FirstName));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenPhoneNumberIsEmpty()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .With(a => a.PhoneNumber, "")
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.FirstName));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenPhoneNumberLengthIsInvalid()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .With(a => a.PhoneNumber, "072781929043")
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.PhoneNumber));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenPhoneNumberIsNotValid()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .With(a => a.PhoneNumber, "8291029123")
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.FirstName));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenPhoneNumberIsNotUnique()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .With(a => a.PhoneNumber, "0712345678")
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.FirstName));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenSocialSecurityNumberIsEmpty()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .With(a => a.SocialSecurityNumber, "")
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.SocialSecurityNumber));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenSocialSecurityNumberLengthIsInvalid()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .With(a => a.SocialSecurityNumber, new string('8', 20))
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.SocialSecurityNumber));
    }

    [Fact]
    public void ValidateEmployee_ThrowValidationTestException_WhenSocialSecurityNumberDoesNotContainOnlyNumbers()
    {
        // arrange
        var employeeForCreationValidatorDto = fixture
            .Build<EmployeeBaseValidatorDto>()
            .With(a => a.SocialSecurityNumber, "aaaa123123123a")
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            employeeValidator.TestValidate(employeeForCreationValidatorDto).ShouldHaveValidationErrorFor(a => a.FirstName));
    }
}