namespace HumanCapitalManagement.API.Validators.Tests.InstitutionValidatorsTests;

public class CreateNewInstitutionValidatorTests : TestBaseValidators
{
    private CreateNewInstitutionValidator validationRules;

    public CreateNewInstitutionValidatorTests()
    {
        validationRules = new CreateNewInstitutionValidator();
    }

    [Fact]
    public void ValidateInstitution_ThrowValidationTestException_WhenNameIsEmpty()
    {
        // arrange
        var createInstitutionValidatorDto = fixture
            .Build<CreateInstitutionValidatorDto>()
            .Without(a => a.Name)
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            validationRules.TestValidate(createInstitutionValidatorDto).ShouldHaveValidationErrorFor(a => a.Name));
    }

    [Fact]
    public void ValidateInstitution_ThrowValidationTestException_WhenContactDetailsIsEmpty()
    {
        // arrange
        var createInstitutionValidatorDto = fixture
            .Build<CreateInstitutionValidatorDto>()
            .Without(a => a.ContactDetails)
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            validationRules.TestValidate(createInstitutionValidatorDto).ShouldHaveValidationErrorFor(a => a.ContactDetails));
    }

    [Fact]
    public void ValidateInstitution_ThrowValidationTestException_WhenNameStartWithLowercaseLetter()
    {
        // arrange
        var createInstitutionValidatorDto = fixture
            .Build<CreateInstitutionValidatorDto>()
            .With(a => a.Name, "name")
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            validationRules.TestValidate(createInstitutionValidatorDto).ShouldHaveValidationErrorFor(a => a.Name));
    }

    [Theory]
    [InlineData("aa")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaa")]
    public void ValidateInstitution_ThrowValidationTestException_WhenNameLengthIsSmallerThan3OrBiggerThan20Characters(string name)
    {
        // arrange
        var createInstitutionValidatorDto = fixture
            .Build<CreateInstitutionValidatorDto>()
            .With(a => a.Name, name)
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            validationRules.TestValidate(createInstitutionValidatorDto).ShouldHaveValidationErrorFor(a => a.Name));
    }

    [Fact]
    public void ValidateInstitution_ThrowValidationTestException_WhenNameDoesntContainOnlyLettersAndSpaces()
    {
        // arrange
        var createInstitutionValidatorDto = fixture
            .Build<CreateInstitutionValidatorDto>()
            .With(a => a.Name, "!@#$%^&*()")
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            validationRules.TestValidate(createInstitutionValidatorDto).ShouldHaveValidationErrorFor(a => a.Name));
    }

    [Fact]
    public void ValidateInstitution_ThrowValidationTestException_WhenContactDetailsDoesntHaveEmailFormat()
    {
        // arrange
        var createInstitutionValidatorDto = fixture
            .Build<CreateInstitutionValidatorDto>()
            .With(a => a.ContactDetails, "contactdetails")
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            validationRules.TestValidate(createInstitutionValidatorDto).ShouldHaveValidationErrorFor(a => a.ContactDetails));
    }

    [Theory]
    [InlineData("aa@gmail.com")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaa@gmail.com")]
    public void ValidateInstitution_ThrowValidationTestException_WhenContactDetailsLengthIsSmallerThan3OrBiggerThan20Characters(string contactDetails)
    {
        // arrange
        var createInstitutionValidatorDto = fixture
            .Build<CreateInstitutionValidatorDto>()
            .With(a => a.ContactDetails, contactDetails)
            .Create();

        // act & assert
        ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
            validationRules.TestValidate(createInstitutionValidatorDto).ShouldHaveValidationErrorFor(a => a.Name));
    }
}
