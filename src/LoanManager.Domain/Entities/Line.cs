using LoanManager.Domain.Entities.Bases;
using LoanManager.Domain.Enums;

namespace LoanManager.Domain.Entities;

public class Line : Auditable
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
    public decimal PendingInterest { get; set; }
    public decimal PendingPrincipal { get; set; }
    public LineType Type { get; set; }
    public decimal CollectedInterest => 
        Amount <= PendingInterest ? Amount : PendingInterest;
    public decimal EndingInterest => PendingInterest - CollectedInterest;
    
    public decimal CollectedPrincipal => 
        EndingInterest > 0 ? 0 : Amount - CollectedInterest;
    public decimal EndingPrincipal => PendingPrincipal - CollectedPrincipal;
}