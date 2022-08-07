using ErrorOr;

using Microsoft.AspNetCore.Mvc;

namespace LoanManager.Presentation.API.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(IList<Error> errors)
    {
        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => throw new NotImplementedException(),
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}