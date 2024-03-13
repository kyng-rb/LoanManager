using LoanManager.Domain.Aggregate;
using LoanManager.Domain.LoanAggregate;

namespace LoanManager.Domain.WithHoldAggregate;

public class Withhold : BaseAggregate
{
    public int LoanId { get; set; }
    public string? Note { get; set; }

    public virtual Loan Loan { get; set; } = null!;
}