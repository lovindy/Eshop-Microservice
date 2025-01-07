using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors
{
    // ValidationBehavior is a class that contains the logic to validate the request.
    // IPipelineBehavior<TRequest, TResponse> is an interface that represents a pipeline behavior.
    public class ValidationBehavior<TRequest, TResponse>
        (IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {

        // Handle is a method that contains the logic to validate the request.
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // 1. Create a new ValidationContext.
            var context = new ValidationContext<TRequest>(request);

            // 2. Validate the request using the validators.
            var validationResults =
                await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // 3. Get failures from the validation results.
            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            // 4. If there are failures, throw a ValidationException.
            if (failures.Any())
                throw new ValidationException(failures);

            // 5. Return the response.
            return await next();
        }
    }
}
