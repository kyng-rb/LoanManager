using LoanManager.Domain.Entities;

namespace LoanManager.Application.Common.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string Generate(User user);
}