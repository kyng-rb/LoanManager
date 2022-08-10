using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Domain.Entities;

namespace LoanManager.Application.Tests.Common.Mocks.Persistence;

public class UserRepositoryMock : IUserRepository
{
    private readonly static List<User> Users = new();

    public void Add(User user)
    {
        Users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return Users.SingleOrDefault(u => u.Email == email);
    }
}