using LoanManager.Domain.CustomerAggregate;
using LoanManager.Domain.Entities.Bases;
using LoanManager.Domain.Enums;

namespace LoanManager.Domain.Entities;

public class Loan : Auditable
{
    public Loan()
    {
        Transactions = new HashSet<Transaction>();
        Withholds = new HashSet<Withhold>();
    }

    public int Id { get; init; }
    public Currency Currency { get; init; }
    public decimal Amount { get; init; }
    public float InterestRate { get; init; }
    public DateOnly Date { get; init; }
    public int CustomerId { get; init; }
    public int UserId { get; init; }

    public virtual ICollection<Transaction>? Transactions { get; set; }
    public virtual ICollection<Withhold>? Withholds { get; set; }
    public virtual Customer Customer { get; set; } = null!;
    public virtual User Owner { get; set; } = null!;
}