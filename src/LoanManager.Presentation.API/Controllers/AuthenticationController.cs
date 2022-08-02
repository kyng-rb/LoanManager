using LoanManager.Presentation.Contracts.Auth;
using LoanManager.Application.Services.Auth;

using Microsoft.AspNetCore.Mvc;

namespace LoanManager.Presentation.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var serviceOutput = _authService.Register(request.FirstName,
                                                  request.LastName,
                                                  request.Email,
                                                  request.Password);
        return Ok(serviceOutput);
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Register(LoginRequest request)
    {
        var serviceOutput = _authService.Login(request.Email,
                                               request.Password);
        return Ok(serviceOutput);
    }
}