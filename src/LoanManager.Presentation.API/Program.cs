using LoanManager.Application;
using LoanManager.Infrastructure;
using LoanManager.Presentation.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddPresentation()
       .AddInfrastructure(builder.Configuration)
       .AddApplication();


var app = builder.Build();
app.ConfigureApplication();
app.Run();