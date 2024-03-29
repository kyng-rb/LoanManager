using ErrorOr;

using LoanManager.Application.Authentication.Common;
using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Domain.Common.Errors;
using LoanManager.Domain.Entities;

using MediatR;

namespace LoanManager.Application.Authentication.Commands.Register;

public class CommandHandler : IRequestHandler<Command, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jWtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public CommandHandler(IJwtTokenGenerator jWtTokenGenerator,
                                  IUserRepository userRepository)
    {
        _jWtTokenGenerator = jWtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(Command command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserByEmail(command.Email) is not null)
            return Errors.User.DuplicateEmail;

        var user = new User
        {
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Password = command.Password
        };

        _userRepository.Add(user);
        var token = _jWtTokenGenerator.Generate(user);

        return new AuthenticationResult(
            User: user,
            Token: token);
    }
}