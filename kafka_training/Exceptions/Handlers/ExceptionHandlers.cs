using kafka_training.Exceptions.Handlers.Base;
using kafka_training.Exceptions.Handlers.Base.Interfaces;
using kafka_training.Exceptions.Handlers.Custom;

namespace kafka_training.Exceptions.Handlers;

internal class ExceptionHandlers : IExceptionHandlerCollection
{
    public IEnumerable<IExceptionHandler> Handlers => _exceptionHandlers;
    private readonly List<IExceptionHandler> _exceptionHandlers;

    public ExceptionHandlers()
    {
        // Custom exception handlers
        this._exceptionHandlers = this.GetCustomExceptionHandlers();

        // Default exception handler
        this._exceptionHandlers.Add(new DefaultExceptionHandler());
    }

    private List<IExceptionHandler> GetCustomExceptionHandlers()
    {
        return
        [
            new OperationCanceledExceptionHandler(),
            new ConsumeExceptionHandler()
        ];
    }

}