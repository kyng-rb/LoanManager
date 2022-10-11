using LoanManager.Domain.Entities.Bases;

namespace LoanManager.Domain.Entities;

public class User : Auditable
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}