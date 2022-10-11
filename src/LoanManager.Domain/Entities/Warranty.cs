namespace LoanManager.Domain.Entities;

public class Warranty
{
    public int Id { get; set; }
    public int LoanId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Uri? Image { get; set; }
}