using System.Reflection;
using LoanManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Scrutor;

namespace LoanManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,
                                                                     ConfigurationManager configuration)
    {
        services.AddDatabase(configuration);
        
        services.Scan(selector => selector
            .FromAssemblies(
                Assembly.GetExecutingAssembly())
            .AddClasses()
            .UsingRegistrationStrategy(RegistrationStrategy.Throw)
            .AsMatchingInterface()
            .WithScopedLifetime());

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