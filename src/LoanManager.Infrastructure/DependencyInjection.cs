using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Application.Common.Interfaces.Services;
using LoanManager.Infrastructure.Persistence;
using LoanManager.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
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
        services.AddScoped<IUserRepository, UserRepository>();

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

    private static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
        
        
        return services;
    }

    public class MyUser : IdentityUser
    {
    }
}