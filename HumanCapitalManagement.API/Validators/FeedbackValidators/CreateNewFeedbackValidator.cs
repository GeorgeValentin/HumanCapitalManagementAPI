using FluentValidation;
using HumanCapitalManagement.Entities.DTOs.FeedbackDTOs;

namespace HumanCapitalManagement.API.Validators.FeedbackValidators;

public class CreateNewFeedbackValidator : AbstractValidator<FeedbackForCreationValidatorDto>
{
    public CreateNewFeedbackValidator()
	{
		RuleFor(elem => elem.Content)
			.NotEmpty()
			.WithMessage("The {Content} cannot be empty!")
			.DependentRules(() =>
			{
				RuleFor(elem => elem.Content)
					.Length(ConstantValues.FEEDBACK_MESSAGE_LOWER_BOUND, ConstantValues.FEEDBACK_MESSAGE_UPPER_BOUND)
					.WithMessage(elem => $"The {{Content}} of the feedback must be between " +
                                $"{ConstantValues.FEEDBACK_MESSAGE_LOWER_BOUND} and {ConstantValues.FEEDBACK_MESSAGE_UPPER_BOUND}" +
                                $" characters. You entered {elem.Content.Length} characters!");

                RuleFor(elem => elem.Content)
                            .Must(a => a.Substring(0, 1).All(Char.IsUpper))
                            .When(a => a.Content.Length > 1)
                            .WithMessage("The {Content} of the feedback must start with Capital letter!");

                RuleFor(elem => elem.IsSent)
                    .NotEmpty()
                    .WithMessage("{IsSent} cannot be empty!");
            });
	}
}
