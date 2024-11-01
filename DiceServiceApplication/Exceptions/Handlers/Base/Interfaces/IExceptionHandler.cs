namespace DiceServiceApplication.Exceptions.Handlers.Base.Interfaces;

public interface IExceptionHandler
{
    bool Handle(Exception exception);
}