using System.Net;
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
        return serviceOutput.MatchFirst(
            result => Ok(MapResult(result)),
            error => Problem(statusCode: (int)HttpStatusCode.Conflict, detail: error.Description));
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Register(LoginRequest request)
    {
        var serviceOutput = _authService.Login(request.Email,
                                               request.Password);
        return serviceOutput.MatchFirst(
            result => Ok(MapResult(result)),
            error => Problem(statusCode: (int)HttpStatusCode.Conflict, detail: error.Description));
    }

    private AuthResponse MapResult(AuthenticationResult result) => new AuthResponse(
        Id: result.User.Id,
        FirstName: result.User.FirstName,
        LastName: result.User.LastName,
        Email: result.User.LastName,
        Token: result.Token);
}