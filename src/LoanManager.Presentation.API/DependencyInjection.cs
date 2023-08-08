using LoanManager.Presentation.API.Common.Mappings;
using LoanManager.Presentation.API.Extensions;

namespace LoanManager.Presentation.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.ConfigureSwagger();
        services.AddMappings();
        return services;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app.ConfigureSwagger();

        app.UseExceptionHandler("/error");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}