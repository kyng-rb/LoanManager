namespace LoanManager.Application.Services.Auth;

public class AuthenticationService : IAuthenticationService
{
    AuthenticationResult IAuthenticationService.Login(string email, string password)
        => new(Id: Guid.NewGuid(),
               FirstName: "Name",
               LastName: "Last",
               Email: email,
               Token: Guid.NewGuid().ToString());

    AuthenticationResult IAuthenticationService.Register(string firstName, string lastName, string email, string password)
    => new(Id: Guid.NewGuid(),
           FirstName: firstName,
           LastName: lastName,
           Email: email,
           Token: Guid.NewGuid().ToString());
}