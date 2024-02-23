using ErrorOr;
using LoanManager.Application.Customer.Common;
using MediatR;

namespace LoanManager.Application.Customer.Commands.Register;

public record Command(string FirstName, string? LastName, string Phone) 
    : IRequest<ErrorOr<CommandResult>>;
