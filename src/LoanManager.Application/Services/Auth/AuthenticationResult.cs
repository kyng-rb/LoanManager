using LoanManager.Domain.Entities;

namespace LoanManager.Application.Services.Auth;

public record AuthenticationResult(User User,
                                   string Token);