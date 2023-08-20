using LoanManager.Domain.Entities.Bases;

namespace LoanManager.Domain.Entities
{
    public class ServerUser : Auditable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServerId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Server Server { get; set; } = null!;
    }
}