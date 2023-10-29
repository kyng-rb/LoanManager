namespace LoanManager.Presentation.RestAPI.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection ConfigureSwaggerService(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    public static WebApplication ConfigureSwaggerPresentation(this WebApplication application)
    {
        application.UseSwagger();
        application.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "default");
            options.RoutePrefix = string.Empty;
        });

        return application;
    }
}