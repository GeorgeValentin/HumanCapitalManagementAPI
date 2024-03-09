using FluentValidation;
using FluentValidation.Validators;
using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.InstitutionDTOs;
using System.Text.RegularExpressions;

namespace HumanCapitalManagement.API.Validators.InstitutionValidators;

public class CreateNewInstitutionValidator : AbstractValidator<CreateInstitutionValidatorDto>
{
	private readonly ApplicationDbContext? _context;

	public CreateNewInstitutionValidator()
	{

	}

	public CreateNewInstitutionValidator(ApplicationDbContext context)
	{
		_context = context;

		RuleFor(a => a.Name)
			.NotEmpty()
			.WithMessage("The {Name} of the institution cannot be empty!")
			.DependentRules(() =>
			{
				RuleFor(a => a.ContactDetails)
					.NotEmpty()
					.WithMessage("The {ContactDetails} field of the instituon field cannot be empty!")
					.DependentRules(() =>
					{
                        RuleFor(p => p.Name)
							.Length(ConstantValues.INSTITUTION_RELATED_NAME_LOWER_LENGTH, ConstantValues.INSTITUTION_RELATED_NAME_UPPER_LENGTH)
                            .WithMessage(elem => $"The {{Name}} of the institution must be between " +
                                $"{ConstantValues.INSTITUTION_RELATED_NAME_LOWER_LENGTH} and {ConstantValues.INSTITUTION_RELATED_NAME_UPPER_LENGTH}" +
                                $" characters. You entered {elem.Name.Length} characters!");

                        RuleFor(p => p.Name)
							.Must(a => a.Substring(0, 1).All(Char.IsUpper))
							.When(a => a.Name.Length > 1)
							.WithMessage("The {Name} of the instituion must start with Capital letter!");

                        RuleFor(p => p.Name)
							.Must(a => Regex.Match(a, @"^[a-zA-Z ]+$").Success)
							.WithMessage("The {Name} of the institution must only contain letters and spaces!");

                        RuleFor(p => p.Name)
							.Must(elem => !_context.Institutions.Any(b => b.Name
								.Equals(elem)))
							.WithMessage("The {Name} of the institution must be unique!");

						RuleFor(p => p.ContactDetails)
							.Length(15, 35);

                        RuleFor(p => p.ContactDetails)
							.EmailAddress(EmailValidationMode.AspNetCoreCompatible)
							.WithMessage("The {ContactDetails} field of the institution is not a valid email address!");

                        RuleFor(p => p.ContactDetails)
							.Must(elem => !_context.Institutions.Any(b => b.ContactDetails
								.Equals(elem)))
							.WithMessage("The {ContactDetails} field of the institution must be unique!");
                    });
			});
	}
}
