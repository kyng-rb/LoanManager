using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Domain.Entities;

namespace LoanManager.Infrastructure.Services;

public class JWTTokenGenerator : IJwtTokenGenerator
{
    public string Generate(User user)
    {
        return Guid.NewGuid().ToString();
    }
}