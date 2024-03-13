using LoanManager.Domain.UserAggregate;

namespace LoanManager.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}