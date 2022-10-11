using LoanManager.Domain.Entities.Bases;
using LoanManager.Domain.Enums;

namespace LoanManager.Domain.Entities;

public class Loan : Auditable
{
    public int Id { get; set; }
    public Currency Currency { get; set; }
    public decimal Amount { get; set; }
    public float InterestRate { get; set; }
    public DateOnly Date { get; set; }
    public int CustomerId { get; set; }
    public int OwnerId { get; set; }
}