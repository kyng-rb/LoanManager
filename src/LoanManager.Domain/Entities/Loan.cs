using LoanManager.Domain.Entities.Bases;
using LoanManager.Domain.Enums;

namespace LoanManager.Domain.Entities;

public class Loan : Auditable
{
    public Loan()
    {
        Transactions = new HashSet<Transaction>();
        Withhelds = new HashSet<Withhold>();
    }

    public int Id { get; init; }
    public Currency Currency { get; init; }
    public decimal Amount { get; init; }
    public float InterestRate { get; init; }
    public DateOnly Date { get; init; }
    public int CustomerId { get; init; }
    public int OwnerId { get; init; }

    public virtual ICollection<Transaction>? Transactions { get; set; }
    public virtual ICollection<Withhold>? Withhelds { get; set; }
    public virtual Customer Customer { get; set; } = null!;
    public virtual Customer Owner { get; set; } = null!;
}