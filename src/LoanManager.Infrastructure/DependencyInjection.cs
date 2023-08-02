using System.Net;
using System.Text;

using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Application.Common.Interfaces.Services;
using LoanManager.Infrastructure.Authentication;
using LoanManager.Infrastructure.Persistence;
using LoanManager.Infrastructure.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LoanManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                       ConfigurationManager configuration)
    {
        services.AddAuthorization(configuration);

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddAuthorization(this IServiceCollection services,
                                                       ConfigurationManager configuration)
    {
        var settings = new JwtSettings();
        configuration.Bind(JwtSettings.FieldName, settings);

        services.AddSingleton(Options.Create(settings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = settings.Issuer,
                ValidAudience = settings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(settings.Secret)
                )
            });

        return services;
    }
}