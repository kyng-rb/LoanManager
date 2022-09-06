using ErrorOr;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

namespace LoanManager.Application.Common.PipelineBehaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    //where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationPipelineBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        if (_validator == null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        return validationResult.IsValid
            ? await next()
            : TryCreateErrorResponseFromErrors(validationResult.Errors, out var errorResponse)
                   ? errorResponse
                   : throw new ValidationException(validationResult.Errors);
    }

    private static bool TryCreateErrorResponseFromErrors(List<ValidationFailure> validationFailures, out TResponse errorResponse)
    {
        List<Error> errors = validationFailures
            .ConvertAll(x => Error.Validation(code: x.PropertyName,
                                                          description: x.ErrorMessage));

        try
        {
            errorResponse = (TResponse)(dynamic)errors;
            return true;
        }
        catch
        {
            errorResponse = default!;
            return false;
        }
    }
}