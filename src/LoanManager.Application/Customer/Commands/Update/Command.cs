using LoanManager.Application.Customer.Common;
using MediatR;
using ErrorOr;

namespace LoanManager.Application.Customer.Commands.Update;

public record Command(int CustomerId, string FirstName, string? LastName, string PhoneNumber) 
    : IRequest<ErrorOr<CommandResult>>;