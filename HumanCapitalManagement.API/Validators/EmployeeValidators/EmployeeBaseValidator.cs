using FluentValidation;
using FluentValidation.Validators;
using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using System.Text.RegularExpressions;

namespace HumanCapitalManagement.API.Validators.EmployeeValidators;

public class EmployeeBaseValidator<T> : AbstractValidator<T> where T : EmployeeBaseValidatorDto
{
	private readonly ApplicationDbContext? _context;

    public EmployeeBaseValidator()
    { }

    public EmployeeBaseValidator(ApplicationDbContext context)
	{
		_context = context;

        RuleFor(elem => elem)
            .NotEmpty()
            .WithMessage("The employee cannot be empty!")
            .DependentRules(() =>
            {
                RuleFor(elem => elem.FirstName)
                    .NotEmpty()
                    .WithMessage("The {FirstName} cannot be empty!")
                    .DependentRules(() =>
                    {
                        RuleFor(elem => elem.FirstName)
                            .Length(ConstantValues.LOWER_BOUND, ConstantValues.EMPLOYEE_NAME_LENGTH_THRESHOLD)
                            .WithMessage(elem => $"The {{FirstName}} must be between " +
                                        $"{ConstantValues.LOWER_BOUND} and {ConstantValues.EMPLOYEE_NAME_LENGTH_THRESHOLD}" +
                                        $" characters. You entered {elem.FirstName.Length} characters!");

                        RuleFor(elem => elem.FirstName)
                            .Must(a => a.Substring(0, 1).All(Char.IsUpper))
                            .When(elem => elem.FirstName.Length > 1)
                            .WithMessage("The {FirstName} must start with Capital letter!");

                        RuleFor(elem => elem.FirstName)
                            .Must(a => Regex.Match(a, @"^[a-zA-Z ]+$").Success)
                            .WithMessage("The {FirstName} must only contain letters and spaces!");
                    });

                RuleFor(elem => elem.LastName)
                    .NotEmpty()
                    .WithMessage("The {LastName} cannot be empty!")
                    .DependentRules(() =>
                    {
                        RuleFor(elem => elem.LastName)
                            .Length(ConstantValues.LOWER_BOUND, ConstantValues.EMPLOYEE_NAME_LENGTH_THRESHOLD)
                            .WithMessage(elem => $"The {{LastName}} must be between " +
                                        $"{ConstantValues.LOWER_BOUND} and {ConstantValues.EMPLOYEE_NAME_LENGTH_THRESHOLD}" +
                                        $" characters. You entered {elem.FirstName.Length} characters!");

                        RuleFor(elem => elem.LastName)
                            .Must(a => a.Substring(0, 1).All(Char.IsUpper))
                            .When(elem => elem.LastName.Length > 1)
                            .WithMessage("The {LastName} must start with Capital letter!");

                        RuleFor(elem => elem.LastName)
                            .Must(a => Regex.Match(a, @"^[a-zA-Z ]+$").Success)
                            .WithMessage("The {LastName} must only contain letters and spaces!");
                    });

                RuleFor(elem => elem.Email)
                    .NotEmpty()
                    .WithMessage("The {Email} cannot be empty!")
                    .DependentRules(() =>
                    {
                        RuleFor(elem => elem.Email)
                            .EmailAddress(EmailValidationMode.AspNetCoreCompatible)
                            .WithMessage("The {Email} entered is invalid!");
                    });

                RuleFor(elem => elem.PhoneNumber)
                  .NotEmpty()
                  .WithMessage("The {PhoneNumber} cannot be empty!")
                  .DependentRules(() =>
                  {
                      RuleFor(elem => elem.PhoneNumber)
                          .Length(ConstantValues.PHONE_LENGTH_THRESHOLD)
                          .WithMessage(elem => $"The {{PhoneNumber}} must consist of " +
                                       $"{ConstantValues.PHONE_LENGTH_THRESHOLD}" +
                                       $" characters. You entered {elem.PhoneNumber.Length} characters!");

                      RuleFor(elem => elem.PhoneNumber)
                          .Must(a => Regex.Match(a, @"^(07)[0-9]{8}$").Success)
                          .WithMessage("The {PhoneNumber} must only consist of numbers and start with '07'");

                  });

                RuleFor(elem => elem.SocialSecurityNumber)
                  .NotEmpty()
                  .WithMessage("The {SocialSecurityNumber} cannot be empty!")
                  .DependentRules(() =>
                  {
                      RuleFor(elem => elem.SocialSecurityNumber)
                          .Length(ConstantValues.SSN_THRESHOLD)
                          .WithMessage(elem => $"The {{SocialSecurityNumber}} must consist of " +
                                       $"{ConstantValues.SSN_THRESHOLD}" +
                                       $" characters. You entered {elem.SocialSecurityNumber.Length} characters!");

                      RuleFor(elem => elem.SocialSecurityNumber)
                          .Must(a => Regex.Match(a, @"^[0-9]+$").Success)
                          .WithMessage("The {SocialSecurityNumber} must only consist of numbers!");
                  });

            });
    }
}
