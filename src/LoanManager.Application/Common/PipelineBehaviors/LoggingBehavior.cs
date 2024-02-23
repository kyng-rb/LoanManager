using ErrorOr;

using LoanManager.Application.Common.Interfaces.Services;

using MediatR;

using Microsoft.Extensions.Logging;

namespace LoanManager.Application.Common.PipelineBehaviors;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger,
    IDateTimeProvider dateTimeProvider)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        logger.LogInformation("Handling {RequestName} at {Now}", typeof(TRequest).Name, dateTimeProvider.UtcNow);

        var response = await next();

        logger.LogInformation("Handling {RequestName} at {Now}", typeof(TRequest).Name, dateTimeProvider.UtcNow);
        return response;
    }
}