namespace kafka_training.Exceptions.Handlers.Base.Interfaces;

public interface IExceptionHandlerCollection
{
    public IEnumerable<IExceptionHandler> Handlers { get; }
}