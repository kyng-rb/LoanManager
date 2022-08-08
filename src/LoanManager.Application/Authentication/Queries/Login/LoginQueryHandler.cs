using ErrorOr;

using LoanManager.Application.Authentication.Common;
using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Domain.Common.Errors;
using LoanManager.Domain.Entities;

using MediatR;

namespace LoanManager.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<RegisterQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJWTTokenGenerator _jWTTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJWTTokenGenerator jWTTokenGenerator, IUserRepository userRepository)
    {
        _jWTTokenGenerator = jWTTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterQuery query, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
            return Errors.Authentication.InvalidCredentials;

        if (user.Password != query.Password)
            return Errors.Authentication.InvalidCredentials;

        var token = _jWTTokenGenerator.Generate(user);

        return new AuthenticationResult(
            User: user,
            Token: token);
    }
}