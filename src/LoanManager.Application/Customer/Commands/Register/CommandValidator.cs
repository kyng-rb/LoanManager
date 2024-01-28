using FluentValidation;

namespace LoanManager.Application.Customer.Commands.Register;

public class CommandValidator: AbstractValidator<Command>
{
    public CommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .NotNull().WithMessage("First Name is required.");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .NotNull().WithMessage("Phone Number is required.")
            .Length(8).WithMessage("PhoneNumber must not be less than 10 characters.");
    }
}