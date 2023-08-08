using LoanManager.Application.Authentication.Commands.Register;
using LoanManager.Application.Authentication.Common;
using LoanManager.Application.Authentication.Queries.Login;
using LoanManager.Presentation.Contracts.Auth;

using Mapster;

namespace LoanManager.Presentation.API.Common.Mappings;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);

        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<LoginRequest, LoginQuery>();
    }
}