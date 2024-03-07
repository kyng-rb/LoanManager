using LoanManager.Domain.Aggregate;

namespace LoanManager.Domain.UserAggregate;

public class User : BaseAggregate
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}