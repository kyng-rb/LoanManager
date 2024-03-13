using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Domain.UserAggregate;

namespace LoanManager.Infrastructure.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    public string Generate(User user)
    {
        return Guid.NewGuid().ToString();
    }
}