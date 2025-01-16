using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger
    ) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
        )
        {
            // Logging the request.
            _logger.LogInformation(
                "[START] Handling request {RequestName} with data: {@RequestData}",
                typeof(TRequest).Name,
                request
            );

            // Measuring performance.
            var stopwatch = Stopwatch.StartNew();

            // Proceed to the next handler in the pipeline.
            var response = await next();

            stopwatch.Stop();
            var timeTaken = stopwatch.Elapsed;

            // Log performance warning if it takes too long.
            if (timeTaken.TotalSeconds > 3)
            {
                _logger.LogWarning(
                    "[PERFORMANCE] Request {RequestName} took {TimeTakenSeconds} seconds",
                    typeof(TRequest).Name,
                    timeTaken.TotalSeconds
                );
            }

            // Logging the response.
            _logger.LogInformation(
                "[END] Finished handling request {RequestName} with response: {@Response}",
                typeof(TRequest).Name,
                response
            );

            return response;
        }
    }
}
