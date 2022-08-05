using Microsoft.AspNetCore.Mvc;

namespace LoanManager.Presentation.API.Controllers;

public class ErrorController : ControllerBase
{
    [HttpPost]
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem(statusCode: 500);
    }
}