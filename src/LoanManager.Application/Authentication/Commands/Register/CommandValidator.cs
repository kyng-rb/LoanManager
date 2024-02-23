using FluentValidation;

namespace LoanManager.Application.Authentication.Commands.Register;

public class CommandValidator : AbstractValidator<Command>
{
    public CommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}