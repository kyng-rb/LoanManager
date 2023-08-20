using LoanManager.Domain.Entities.Bases;

namespace LoanManager.Domain.Entities
{
    public class ServerCustomer : Auditable
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ServerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Server Server { get; set; } = null!;
    }
}