using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Application.Common.Interfaces.Services;
using LoanManager.Infrastructure.Authentication;
using LoanManager.Infrastructure.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoanManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JWTSettings>(configuration.GetSection(JWTSettings.FieldName));

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IJWTTokenGenerator, JWTTokenGenerator>();
        return services;
    }
}