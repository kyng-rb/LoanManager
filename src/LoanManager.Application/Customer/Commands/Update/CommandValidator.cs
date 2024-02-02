using FluentValidation;

namespace LoanManager.Application.Customer.Commands.Update;

public class CommandValidator : AbstractValidator<Command>
{
    public CommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer Id is required.")
            .LessThanOrEqualTo(0).WithMessage("Invalid Customer Id value");
        
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone Number is required.")
            .Length(8).WithMessage("PhoneNumber must have 8 characters.")
            .Matches("[0-9]{8}$").WithMessage("PhoneNumber must have the following format ########");
    }
}