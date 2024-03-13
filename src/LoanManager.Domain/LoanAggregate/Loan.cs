using LoanManager.Domain.Aggregate;
using LoanManager.Domain.CustomerAggregate;
using LoanManager.Domain.LoanAggregate.Enums;
using LoanManager.Domain.TransactionAggregate;
using LoanManager.Domain.UserAggregate;
using LoanManager.Domain.WithHoldAggregate;

namespace LoanManager.Domain.LoanAggregate;

public class Loan : BaseAggregate
{
    public Loan()
    {
        Transactions = new HashSet<Transaction>();
        Withholds = new HashSet<Withhold>();
    }

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