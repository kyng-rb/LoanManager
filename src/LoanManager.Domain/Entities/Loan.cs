using LoanManager.Domain.Entities.Bases;
using LoanManager.Domain.Enums;

namespace LoanManager.Domain.Entities;

public class Loan : Auditable
{
    public Loan()
    {
        Transactions = new HashSet<Transaction>();
        Warranties = new HashSet<Warranty>();
    }

    public int Id { get; init; }
    public Currency Currency { get; init; }
    public decimal Amount { get; init; }
    public float InterestRate { get; init; }
    public DateOnly Date { get; init; }
    public int CustomerId { get; init; }
    public int OwnerId { get; init; }

    public virtual ICollection<Transaction> Transactions { get; set; }
    public virtual ICollection<Warranty>? Warranties { get; set; }
    public virtual Person Customer { get; set; } = null!;
    public virtual Person Owner { get; set; } = null!;
}