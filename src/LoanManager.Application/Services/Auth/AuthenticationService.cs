using ErrorOr;

using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Common.Errors;

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

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not User user)
            return Errors.Authentication.InvalidCredentials;

        if (user.Password != password)
            return Errors.Authentication.InvalidCredentials;

        var token = _jWTTokenGenerator.Generate(user);

        return new AuthenticationResult(
            User: user,
            Token: token);
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not null)
            return Errors.User.DuplicateEmail;

        var user = new User()
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Password = password
        };

        _userRepository.Add(user);
        var token = _jWTTokenGenerator.Generate(user);

        return new AuthenticationResult(
            User: user,
            Token: token);
    }
}