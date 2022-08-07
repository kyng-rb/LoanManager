using LoanManager.Domain.Entities;

namespace LoanManager.Application.Authentication.Common;

public record AuthenticationResult(User User,
                                   string Token);