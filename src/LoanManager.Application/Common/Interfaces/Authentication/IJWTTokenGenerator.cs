namespace LoanManager.Application.Common.Interfaces.Authentication;
public interface IJWTTokenGenerator
{
    string Generate(Guid id, string firstName, string lastName);
}