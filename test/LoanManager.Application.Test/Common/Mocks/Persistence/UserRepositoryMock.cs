using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Domain.UserAggregate;

namespace LoanManager.Application.Test.Common.Mocks.Persistence;

public class UserRepositoryMock : IUserRepository
{
    private readonly List<User> _users;

    public UserRepositoryMock(User user)
    {
        _users = new List<User>
        {
            user
        };
    }

    public UserRepositoryMock()
    {
        _users = new List<User>();
    }

    public void Add(User user)
    {
        user.Id = _users.Count + 1;
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(u => u.Email == email);
    }
}