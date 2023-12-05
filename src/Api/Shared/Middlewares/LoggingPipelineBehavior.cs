namespace SPW.Admin.Api.Shared.Middlewares;

internal sealed class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : class
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling {@RequestType} at {@DateTime}", typeof(TRequest).Name, DateTime.UtcNow);

        var response = await next();

        _logger.LogInformation("Handling {@ResponseType} at {@DateTime}", typeof(TResponse).Name, DateTime.UtcNow);

        return response;
    }
}