namespace HumanCapitalManagement.API.Validators.Tests.InstitutionValidatorsTests
{
    public class InstitutionExistanceValidatorTests : TestBaseValidators
    {
        private InstitutionExistanceValidator institutionExistanceValidator;

        public InstitutionExistanceValidatorTests()
        {
            institutionExistanceValidator = new InstitutionExistanceValidator();
        }

        [Fact]
        public void ValidateInstitution_ThrowValidationTestException_WhenInstitutionIsEmpty()
        {
            // arrange
            var createInstitutionValidatorDto = fixture
                .Build<InstitutionExistanceValidatorDto>()
                .With(a => a.Institution, new Institution())
                .Create();

            // act & assert
            ValidationTestException validationTestException = Assert.Throws<ValidationTestException>(() =>
                institutionExistanceValidator.TestValidate(createInstitutionValidatorDto).ShouldHaveValidationErrorFor(a => a.Institution));
        }
    }
}
