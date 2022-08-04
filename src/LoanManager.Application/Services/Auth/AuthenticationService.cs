using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Domain.Entities;

namespace LoanManager.Application.Services.Auth;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJWTTokenGenerator _jWTTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationService(IJWTTokenGenerator jWTTokenGenerator, IUserRepository userRepository)
    {
        _jWTTokenGenerator = jWTTokenGenerator;
        _userRepository = userRepository;
    }

    AuthenticationResult IAuthenticationService.Login(string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not User user)
            throw new Exception("User does not exists.");

        if (user.Password != password)
            throw new Exception("Password does not match");

        var token = _jWTTokenGenerator.Generate(user);

        return new(user: user,
                   Token: token);
    }

    AuthenticationResult IAuthenticationService.Register(string firstName, string lastName, string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not null)
            throw new Exception("User already exists.");

        var user = new User()
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Password = password
        };

        _userRepository.Add(user);
        var token = _jWTTokenGenerator.Generate(user);

        return new(user: user,
                   Token: token);
    }
}