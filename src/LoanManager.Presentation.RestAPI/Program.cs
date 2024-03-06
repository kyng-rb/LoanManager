using LoanManager.Application;
using LoanManager.Infrastructure;
using LoanManager.Infrastructure.Persistence;
using LoanManager.Presentation.RestAPI.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .ConfigurePresentationServices()
       .ConfigureInfrastructureServices(builder.Configuration)
       .ConfigureApplicationServices();

var app = builder.Build();
app.ConfigureWebApplication();

var serviceProvider = app.Services;

var databaseContext = serviceProvider.GetRequiredService<DatabaseContext>();

await databaseContext.Database.EnsureDeletedAsync();
await databaseContext.Database.MigrateAsync();

app.Run();