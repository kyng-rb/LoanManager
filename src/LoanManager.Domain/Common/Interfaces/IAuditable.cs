namespace LoanManager.Domain.Common.Interfaces;

public interface IAuditable
{
    int CreatedBy { get; set; }
    DateTime CreatedAt { get; init; }
    int? UpdatedBy { get; init; }
    DateTime? UpdatedAt { get; init; }
}