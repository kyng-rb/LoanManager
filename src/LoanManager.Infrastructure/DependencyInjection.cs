using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Application.Common.Interfaces.Services;
using LoanManager.Infrastructure.Persistence;
using LoanManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LoanManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
                                                                     ConfigurationManager configuration)
    {
        services.AddDatabase(configuration);

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services,
                                                  ConfigurationManager configuration)
    {
        const string databaseConnection = "Postgres";

        services.AddDbContext<DatabaseContext>(
                                               options =>
                                                   options
                                                       .UseNpgsql(configuration.GetConnectionString(databaseConnection))
                                                       .LogTo(Console.WriteLine, LogLevel.Information)
                                                       .EnableSensitiveDataLogging()
                                                       .EnableDetailedErrors()
                                              );
        return services;
    }
}