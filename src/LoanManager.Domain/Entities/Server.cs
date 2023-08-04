using LoanManager.Domain.Entities.Bases;

namespace LoanManager.Domain.Entities;

public class Server : Auditable
{
    public Server(string name, int userId)
    {
        Name = name;
        UserId = userId;
    }

    public int Id { get; set; }
    public required string Name { get; set; }
    public required int UserId { get; init; }

    public virtual User User { get; set; } = null!;
}