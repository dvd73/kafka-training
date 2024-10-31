namespace kafka_training.Exceptions.Handlers.Base.Interfaces;

public interface IExceptionHandler
{
    bool Handle(Exception exception);
}