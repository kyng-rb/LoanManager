using LoanManager.Application;
using LoanManager.Infrastructure;
using LoanManager.Presentation.RestAPI.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .ConfigurePresentationServices()
       .ConfigureInfrastructureServices(builder.Configuration)
       .ConfigureApplicationServices();

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);

var app = builder.Build();
app.ConfigureWebApplication();

app.MapGet("/Home", () =>
{
    throw new Exception("The most generic possible exception");
    return "Hello World!";
});

app.Run();