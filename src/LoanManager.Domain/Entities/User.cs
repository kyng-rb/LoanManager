using LoanManager.Domain.Entities.Bases;

namespace LoanManager.Domain.Entities;

public class User : Auditable
{
    public int Id { get; set; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}