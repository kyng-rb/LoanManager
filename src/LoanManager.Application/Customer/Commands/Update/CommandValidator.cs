using FluentValidation;
using LoanManager.Application.Customer.Common;

namespace LoanManager.Application.Customer.Commands.Update;

public class CommandValidator : AbstractValidator<Command>
{
    public CommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("Invalid Customer Id value");
        
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required.");

        RuleFor(x => x.Phone).PhoneMustHaveNicaraguanFormat();
    }
}