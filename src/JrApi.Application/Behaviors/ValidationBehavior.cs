using FluentValidation;
using FluentValidation.Results;
using JrApi.Domain.Core.Abstractions.Results;
using MediatR;

namespace JrApi.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : IResult, new()
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators != null)
        {
            Result validationResult = await ValidateAsync(request, cancellationToken);

            if(validationResult.IsFailure) 
            {
                TResponse result = new();

                foreach (var error in validationResult.Errors!)
                {
                    result.Errors.Add(error);
                }

                return result;
            }
        }

        return await next();
    }

    private async Task<Result> ValidateAsync(TRequest request, CancellationToken cancellationToken)
    {
        ValidationContext<TRequest> context = new(request);
        ValidationResult[] validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        List<ValidationFailure> failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        if (failures.Count != 0)
        {
            List<Error> errors = failures.Select(failure => Error.Create(failure.ErrorCode, failure.ErrorMessage)).ToList();

            return Result.Failure(errors);
        }

        return Result.Success();
    }
}
