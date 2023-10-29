using Carter;

namespace LoanManager.Presentation.RestAPI.Loans;

public class LoansModule : CarterModule
{
    private const string Route = "Loans";

    public LoansModule()
        : base(Route)
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => Results.Ok(Route));
        app.MapPost("/", () => Results.Ok(Route));
    }
}