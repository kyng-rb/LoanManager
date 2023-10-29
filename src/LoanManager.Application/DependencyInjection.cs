using System.Reflection;
using FluentValidation;
using LoanManager.Application.Common.PipelineBehaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LoanManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddScoped(
                           typeof(IPipelineBehavior<,>),
                           typeof(LoggingBehavior<,>));
        services.AddScoped(
                           typeof(IPipelineBehavior<,>),
                           typeof(ValidationPipelineBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}