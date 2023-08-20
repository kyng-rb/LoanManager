using LoanManager.Domain.Entities.Bases;

namespace LoanManager.Domain.Entities;

public class Customer : Auditable
{
    public int Id { get; set; }
    public string FirstName { get; init; } = null!;
    public string? LastName { get; init; }
    public string? Phone { get; init; }
    public int UserId { get; set; }

    public User? User { get; set; }
    public ICollection<Loan>? Loans { get; set; }
    public ICollection<Loan>? Lends { get; set; }
}