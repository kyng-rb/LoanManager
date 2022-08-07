using System.Net;

using LoanManager.Application.Authentication.Commands.Register;
using LoanManager.Application.Authentication.Common;
using LoanManager.Application.Authentication.Queries.Login;
using LoanManager.Presentation.Contracts.Auth;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace LoanManager.Presentation.API.Controllers;

[Route("api/auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _sender;

    public AuthenticationController(ISender mediator)
    {
        _sender = mediator;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        var serviceOutput = await _sender.Send(command);
        return serviceOutput.Match(
            result => Ok(MapResult(result)),
            errors => Problem(errors));
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new RegisterQuery(
            request.Email,
            request.Password);

        var serviceOutput = await _sender.Send(query);

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