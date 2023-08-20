using LoanManager.Domain.Entities.Bases;

namespace LoanManager.Domain.Entities
{
    public class Withheld : Auditable
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public string? Note { get; set; }

        public virtual Loan Loan { get; set; } = null!;
    }
}