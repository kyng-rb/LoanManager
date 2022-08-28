using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Application.Common.Interfaces.Services;
using LoanManager.Infrastructure.Authentication;
using LoanManager.Infrastructure.Persistence;
using LoanManager.Infrastructure.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoanManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                       ConfigurationManager configuration)
    {
        services.AddJwtToken(configuration);
        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IJWTTokenGenerator, JWTTokenGenerator>();

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    private static IServiceCollection AddJwtToken(this IServiceCollection services,
                                                  ConfigurationManager configuration)
    {
        services.Configure<JWTSettings>(configuration.GetSection(JWTSettings.FieldName));

        return services;
    }
}