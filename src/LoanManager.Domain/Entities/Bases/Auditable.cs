namespace LoanManager.Domain.Entities.Bases;

public class Auditable
{
    public int CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public int? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
}