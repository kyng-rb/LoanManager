using ErrorOr;

using LoanManager.Application.Common.Interfaces.Services;

using MediatR;

using Microsoft.Extensions.Logging;

namespace LoanManager.Application.Common.PipelineBehaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger,
                           IDateTimeProvider dateTimeProvider)
    {
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation("Handling {RequestName} at {Now}", typeof(TRequest).Name, _dateTimeProvider.UtcNow);

        var response = await next();

        _logger.LogInformation("Handling {RequestName} at {Now}", typeof(TRequest).Name, _dateTimeProvider.UtcNow);
        return response;
    }
}