using FluentValidation;

namespace LoanManager.Application.Customer.Commands.Register;

public class CommandValidator: AbstractValidator<Command>
{
    public CommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone Number is required.")
            .Length(8).WithMessage("PhoneNumber must have 8 characters.")
            .Matches("[0-9]{8}$").WithMessage("PhoneNumber must have the following format ########");
    }
}