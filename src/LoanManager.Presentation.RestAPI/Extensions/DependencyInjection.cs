using Carter;

namespace LoanManager.Presentation.RestAPI.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection ConfigurePresentationServices(this IServiceCollection services)
    {
        services.ConfigureSwaggerService();
        services.AddProblemDetails();
        services.AddCarter();
        return services;
    }

    public static WebApplication ConfigureWebApplication(this WebApplication webApplication)
    {
        webApplication.UseExceptionHandler();
        webApplication.UseHttpsRedirection();
        webApplication.ConfigureSwaggerPresentation();
        webApplication.MapCarter();

        return webApplication;
    }
}