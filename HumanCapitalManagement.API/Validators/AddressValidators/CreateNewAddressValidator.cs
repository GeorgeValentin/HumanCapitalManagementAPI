using FluentValidation;
using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.AddressDTOs;
using System.Text.RegularExpressions;

namespace HumanCapitalManagement.API.Validators.AddressValidators;

public class CreateNewAddressValidator : AbstractValidator<AddressForCreationValidatorDto>
{
    private readonly ApplicationDbContext _context;

    public CreateNewAddressValidator(ApplicationDbContext context)
    {
        _context = context;

        RuleFor(elem => elem)
            .NotEmpty()
            .WithMessage("The address cannot be empty!")
            .DependentRules(() =>
            {

                RuleFor(elem => elem.StreetName)
                    .NotEmpty()
                    .WithMessage("The {StreetName} cannot be empty!")
                    .DependentRules(() =>
                    {
                        RuleFor(elem => elem.StreetName)
                            .Length(ConstantValues.LOWER_BOUND, ConstantValues.UPPER_BOUND)
                            .WithMessage(elem => $"The {{StreetName}} must be between " +
                                        $"{ConstantValues.LOWER_BOUND} and {ConstantValues.UPPER_BOUND}" +
                                        $" characters. You entered {elem.StreetName.Length} characters!");

                        RuleFor(elem => elem.StreetName)
                            .Must(a => a.Substring(0, 1).All(Char.IsUpper))
                            .When(elem => elem.StreetName.Length > 1)
                            .WithMessage("The {StreetName} must start with Capital letter!");

                        RuleFor(elem => elem.StreetName)
                            .Must(a => Regex.Match(a, @"^[a-zA-Z0-9 ]+$").Success)
                            .WithMessage("The {StreetName} must only contain letters, numbers and spaces!");
                    });


                RuleFor(elem => elem.StreetNumber)
                    .NotEmpty()
                    .WithMessage("The {StreetNumber} cannot be empty!")
                    .DependentRules(() =>
                    {
                        RuleFor(elem => elem.StreetNumber)
                            .Must(a => Regex.Match(a, @"^[0-9]+$").Success)
                            .WithMessage("The {StreetNumber} must contain only numbers!");
                    });

                RuleFor(elem => elem.PostalCode)
                    .NotEmpty()
                    .WithMessage("The {PostalCode} cannot be empty!")
                    .DependentRules(() =>
                    {
                        RuleFor(elem => elem.PostalCode)
                            .Must(a => Regex.Match(a, @"^[0-9]+$").Success)
                            .WithMessage("The {PostalCode} must contain only numbers!");

                        RuleFor(elem => elem.PostalCode)
                            .Length(ConstantValues.LOWER_BOUND, ConstantValues.UPPER_BOUND)
                            .WithMessage(elem => $"The {{PostalCode}} must be between " +
                                        $"{ConstantValues.LOWER_BOUND} and {ConstantValues.UPPER_BOUND}" +
                                        $" characters. You entered {elem.PostalCode.Length} characters!");
                    });

                RuleFor(elem => elem.City)
                    .NotEmpty()
                    .WithMessage("The {City} cannot be empty!")
                    .DependentRules(() =>
                    {
                        RuleFor(elem => elem.City)
                            .Length(ConstantValues.LOWER_BOUND, ConstantValues.UPPER_BOUND)
                            .WithMessage(elem => $"The {{City}} must be between " +
                                        $"{ConstantValues.LOWER_BOUND} and {ConstantValues.UPPER_BOUND}" +
                                        $" characters. You entered {elem.StreetName.Length} characters!");

                        RuleFor(elem => elem.City)
                            .Must(a => a.Substring(0, 1).All(Char.IsUpper))
                            .When(elem => elem.City.Length > 1)
                            .WithMessage("The {City} must start with Capital letter!");

                        RuleFor(elem => elem.City)
                            .Must(a => Regex.Match(a, @"^[a-zA-Z ]+$").Success)
                            .WithMessage("The {City} must only contain letters and spaces!");
                    });

                RuleFor(elem => elem.Country)
                    .NotEmpty()
                    .WithMessage("The {Country} cannot be empty!")
                    .DependentRules(() =>
                    {
                        RuleFor(elem => elem.Country)
                            .Length(ConstantValues.LOWER_BOUND, ConstantValues.UPPER_BOUND)
                            .WithMessage(elem => $"The {{Country}} must be between " +
                                        $"{ConstantValues.LOWER_BOUND} and {ConstantValues.UPPER_BOUND}" +
                                        $" characters. You entered {elem.StreetName.Length} characters!");

                        RuleFor(elem => elem.Country)
                            .Must(a => a.Substring(0, 1).All(Char.IsUpper))
                            .When(elem => elem.Country.Length > 1)
                            .WithMessage("The {Country} must start with Capital letter!");

                        RuleFor(elem => elem.Country)
                            .Must(a => Regex.Match(a, @"^[a-zA-Z ]+$").Success)
                            .WithMessage("The {Country} must only contain letters and spaces!");
                    });
            });
    }
}
