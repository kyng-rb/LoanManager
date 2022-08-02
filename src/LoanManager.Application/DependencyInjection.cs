using LoanManager.Application.Services.Auth;

using Microsoft.Extensions.DependencyInjection;

namespace LoanManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        return services;
    }
}