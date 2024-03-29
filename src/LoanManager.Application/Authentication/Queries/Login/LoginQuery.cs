using ErrorOr;

using LoanManager.Application.Authentication.Common;

using MediatR;

namespace LoanManager.Application.Authentication.Queries.Login;

public record LoginQuery(
string Email,
string Password) : IRequest<ErrorOr<AuthenticationResult>>;