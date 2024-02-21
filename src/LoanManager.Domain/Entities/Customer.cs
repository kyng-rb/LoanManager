using LoanManager.Domain.Entities.Bases;

namespace LoanManager.Domain.Entities;

public class Customer : Auditable
{
    public int Id { get; set; }
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
    public required string Phone { get; init; }
}