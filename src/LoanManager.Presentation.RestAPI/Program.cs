using LoanManager.Application;
using LoanManager.Infrastructure;
using LoanManager.Presentation.RestAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .ConfigurePresentationServices()
       .ConfigureInfrastructureServices(builder.Configuration)
       .ConfigureApplicationServices();

var app = builder.Build();
app.ConfigureWebApplication();

app.Run();