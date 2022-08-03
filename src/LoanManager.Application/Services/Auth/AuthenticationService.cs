using LoanManager.Application.Common.Interfaces.Authentication;

namespace LoanManager.Application.Services.Auth;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJWTTokenGenerator _jWTTokenGenerator;

    public AuthenticationService(IJWTTokenGenerator jWTTokenGenerator)
    {
        _jWTTokenGenerator = jWTTokenGenerator;
    }

    AuthenticationResult IAuthenticationService.Login(string email, string password)
        => new(Id: Guid.NewGuid(),
               FirstName: "Name",
               LastName: "Last",
               Email: email,
               Token: Guid.NewGuid().ToString());

    AuthenticationResult IAuthenticationService.Register(string firstName, string lastName, string email, string password)
    {
        var userId = Guid.NewGuid();

        var token = _jWTTokenGenerator.Generate(id: userId, firstName, lastName);

        return new(Id: userId,
                   FirstName: firstName,
                   LastName: lastName,
                   Email: email,
                   Token: token);
    }
}