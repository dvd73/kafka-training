using kafka_training.Exceptions.Handlers.Base.Interfaces;
using Microsoft.Extensions.Logging;

namespace kafka_training.Exceptions.Handlers.Base;

internal class AppExceptionHandler
{
    private readonly ILogger<AppExceptionHandler> _logger;
    private readonly IExceptionHandlerCollection _handlerCollection;

    public AppExceptionHandler(ILogger<AppExceptionHandler> logger, IExceptionHandlerCollection handlerCollection)
    {
        _logger = logger;
        _handlerCollection = handlerCollection;
    }
    internal void HandleException(Exception exception)
    {
        if (exception.GetType() != typeof(UnauthorizedAccessException))
            _logger.LogError(exception, exception.Message);

        foreach (var handler in _handlerCollection.Handlers)
        {
            var handled = handler.Handle(exception);
            if (handled)
                return;
        }
    }
}