using LoanManager.Domain.Common.Interfaces;

namespace LoanManager.Domain.Aggregate;

public abstract class BaseAggregate : IAggregate, IIdentifiable, IDeletable, IAuditable
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; init; }
    public int? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
}