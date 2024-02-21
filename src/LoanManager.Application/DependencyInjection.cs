using System.Reflection;
using FluentValidation;
using LoanManager.Application.Common.PipelineBehaviors;
using MediatR;
using Scrutor;
using Microsoft.Extensions.DependencyInjection;

namespace LoanManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddSingleton(
                           typeof(IPipelineBehavior<,>),
                           typeof(LoggingBehavior<,>));
        services.AddSingleton(
                              typeof(IPipelineBehavior<,>),
                              typeof(ValidationPipelineBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.Scan(selector => selector
            .FromAssemblies(
                Assembly.GetExecutingAssembly())
            .AddClasses()
            .UsingRegistrationStrategy(RegistrationStrategy.Throw)
            .AsMatchingInterface()
            .WithScopedLifetime());
        
        return services;
    }
}