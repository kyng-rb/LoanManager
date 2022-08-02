using LoanManager.Presentation.API;
using LoanManager.Infrastructure;
using LoanManager.Application;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
            .AddPresentation()
            .AddInfrastructure()
            .AddApplication();
}

var app = builder.Build();
{
    app.ConfigureApplication();
    app.Run();
}