using Microsoft.AspNetCore.Diagnostics;

using static System.Net.Mime.MediaTypeNames;

namespace LoanManager.Presentation.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //     app.UseExceptionHandler(exceptionHandlerApp =>
        // {
        //     exceptionHandlerApp.Run(async context =>
        //     {
        //         context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        //         // using static System.Net.Mime.MediaTypeNames;
        //         context.Response.ContentType = Text.Plain;

        //         await context.Response.WriteAsync("An exception was thrown.");

        //         var exceptionHandlerPathFeature =
        //             context.Features.Get<IExceptionHandlerPathFeature>();

        //         if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
        //         {
        //             await context.Response.WriteAsync(" The file was not found.");
        //         }

        //         if (exceptionHandlerPathFeature?.Path == "/")
        //         {
        //             await context.Response.WriteAsync(" Page: Home.");
        //         }
        //     });
        // });

        app.UseExceptionHandler("/error");
        app.UseHttpsRedirection();
        app.MapControllers();

        return app;
    }
}