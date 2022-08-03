using LoanManager.Presentation.API;
using LoanManager.Infrastructure;
using LoanManager.Application;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
            .AddPresentation()
            .AddInfrastructure(builder.Configuration)
            .AddApplication();
}

var app = builder.Build();
{
    app.ConfigureApplication();
    app.Run();
}