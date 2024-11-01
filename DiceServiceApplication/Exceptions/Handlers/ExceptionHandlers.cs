using DiceServiceApplication.Exceptions.Handlers.Base;
using DiceServiceApplication.Exceptions.Handlers.Base.Interfaces;
using DiceServiceApplication.Exceptions.Handlers.Custom;

namespace DiceServiceApplication.Exceptions.Handlers;

internal class ExceptionHandlers : IExceptionHandlerCollection
{
    public IEnumerable<IExceptionHandler> Handlers => _exceptionHandlers;
    private readonly List<IExceptionHandler> _exceptionHandlers;

    public ExceptionHandlers()
    {
        // Custom exception handlers
        _exceptionHandlers = GetCustomExceptionHandlers();

        // Default exception handler
        _exceptionHandlers.Add(new DefaultExceptionHandler());
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