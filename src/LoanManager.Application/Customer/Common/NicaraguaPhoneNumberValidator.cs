using FluentValidation;

namespace LoanManager.Application.Customer.Common;

public class NicaraguaPhoneNumberValidator : AbstractValidator<string>
{
    public NicaraguaPhoneNumberValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage("Phone Number is required.")
            .Length(8).WithMessage("PhoneNumber must have 8 characters.")
            .Matches("[0-9]{8}$").WithMessage("PhoneNumber must have the following format ########");
    }
}