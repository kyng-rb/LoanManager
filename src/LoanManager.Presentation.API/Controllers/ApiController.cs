using ErrorOr;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LoanManager.Presentation.API.Controllers;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(IList<Error> errors)
    {
        return errors.Count == 0
            ? Problem()
            : GetErrors(errors);
    }

    private IActionResult GetErrors(IList<Error> errors)
    {
        return errors.All(x => x.Type == ErrorType.Validation)
                   ? ValidationProblem(errors)
                   : ProblemResult(errors);
    }

    private IActionResult ProblemResult(IList<Error> errors)
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

    private IActionResult ValidationProblem(IList<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (Error error in errors)
        {
            modelStateDictionary.AddModelError(error.Code,
                                                  error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }
}