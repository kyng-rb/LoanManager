namespace LoanManager.Domain.Entities;

public class Warranty
{

    public int Id { get; init; }
    public int LoanId { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public Uri? Image { get; init; }

    public virtual Loan Loan { get; set; } = null!;
}