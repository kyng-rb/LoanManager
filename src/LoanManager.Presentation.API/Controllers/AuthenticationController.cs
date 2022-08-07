using System.Net;

using LoanManager.Application.Services.Auth;
using LoanManager.Presentation.Contracts.Auth;

using Microsoft.AspNetCore.Mvc;

namespace LoanManager.Presentation.API.Controllers;

[Route("api/auth")]
public class AuthenticationController : ApiController
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
        return serviceOutput.Match(
            result => Ok(MapResult(result)),
            errors => Problem(errors));
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Register(LoginRequest request)
    {
        var serviceOutput = _authService.Login(request.Email,
                                               request.Password);
        return serviceOutput.Match(
            result => Ok(MapResult(result)),
            errors => Problem(errors));
    }

    private AuthResponse MapResult(AuthenticationResult result) => new(
        Id: result.User.Id,
        FirstName: result.User.FirstName,
        LastName: result.User.LastName,
        Email: result.User.LastName,
        Token: result.Token);
}