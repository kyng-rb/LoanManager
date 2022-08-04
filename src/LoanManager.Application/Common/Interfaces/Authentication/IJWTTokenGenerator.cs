using LoanManager.Domain.Entities;

namespace LoanManager.Application.Common.Interfaces.Authentication;
public interface IJWTTokenGenerator
{
    string Generate(User user);
}