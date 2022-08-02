namespace LoanManager.Presentation.Contracts.Auth;

public record LoginRequest(string Email,
						   string Password);