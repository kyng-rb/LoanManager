using Microsoft.AspNetCore.Mvc;

namespace LoanManager.Presentation.API.Controllers;

[Route("api/[controller]")]
public class LoansController : ApiController
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Nice Temporal message.");
    }
}