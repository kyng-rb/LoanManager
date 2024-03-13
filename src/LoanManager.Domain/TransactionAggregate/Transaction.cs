using LoanManager.Domain.Aggregate;
using LoanManager.Domain.LoanAggregate;
using LoanManager.Domain.TransactionAggregate.Enums;

namespace LoanManager.Domain.TransactionAggregate;

public class Transaction : BaseAggregate
{
    public int LoanId { get; init; }
    public DateOnly Date { get; init; }
    public TransactionType Type { get; init; }
    public decimal Amount { get; init; }
    
    public decimal Interest { get; init; }
    public decimal InterestCollected { get; init; }
    public decimal InterestRemaining { get; init; }
    
    public decimal Principal { get; init; }
    public decimal PrincipalCollected { get; init; }
    public decimal PrincipalRemaining { get; init; }

    public virtual Loan Loan { get; set; } = null!;
}