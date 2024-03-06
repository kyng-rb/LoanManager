using LoanManager.Domain.Aggregate;

namespace LoanManager.Domain.CustomerAggregate;

public class Customer : BaseAggregate
{
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
    public required string Phone { get; init; }
}