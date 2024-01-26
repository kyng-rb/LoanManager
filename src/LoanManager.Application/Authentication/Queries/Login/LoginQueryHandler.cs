using ErrorOr;

using LoanManager.Application.Authentication.Common;
using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Domain.Common.Errors;
using LoanManager.Domain.Entities;

using MediatR;

namespace LoanManager.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jWtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jWtTokenGenerator, IUserRepository userRepository)
    {
        _jWtTokenGenerator = jWtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserByEmail(query.Email) is not User user)
            return Errors.Authentication.InvalidCredentials;

        if (user.Password != query.Password)
            return Errors.Authentication.InvalidCredentials;

        var token = _jWtTokenGenerator.Generate(user);

        return new AuthenticationResult(
            User: user,
            Token: token);
    }
}