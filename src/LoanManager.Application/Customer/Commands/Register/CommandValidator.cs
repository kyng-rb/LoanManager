using FluentValidation;
using LoanManager.Application.Customer.Common;

namespace LoanManager.Application.Customer.Commands.Register;

public class CommandValidator: AbstractValidator<Command>
{
    public CommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required.");

        RuleFor(x => x.Phone).SetValidator(new NicaraguaPhoneNumberValidator());
    }
}