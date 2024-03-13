using LoanManager.Domain.UserAggregate;

namespace LoanManager.Application.Authentication.Common;

public record AuthenticationResult(User User,
                                   string Token);