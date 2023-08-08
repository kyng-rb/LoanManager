using ErrorOr;

using LoanManager.Application.Authentication.Common;

using MediatR;

namespace LoanManager.Application.Authentication.Commands.Register;

public record RegisterCommand(
string FirstName,
string LastName,
string Email,
string Password) : IRequest<ErrorOr<AuthenticationResult>>;