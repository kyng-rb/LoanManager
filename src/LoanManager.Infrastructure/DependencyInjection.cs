using System.Reflection;
using Ardalis.Specification;
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
        services.AddScoped(typeof(IRepositoryBase<>), typeof(GenericRepository<>));
        
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
        var connectionString = configuration.GetConnectionString(databaseConnection);
        
        services.AddDbContext<DatabaseContext>(
                                               options =>
                                                   options
                                                       .UseNpgsql(connectionString)
                                                       .LogTo(Console.WriteLine, LogLevel.Information)
                                                       .EnableSensitiveDataLogging()
                                                       .EnableDetailedErrors()
                                              , ServiceLifetime.Singleton
                                              );
        return services;
    }
}