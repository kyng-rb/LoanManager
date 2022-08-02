using LoanManager.Presentation.API;

var builder = WebApplication.CreateBuilder(args);
{
	builder.Services.AddPresentation();
}

var app = builder.Build();
{
	app.ConfigureApplication();
	app.Run();
}