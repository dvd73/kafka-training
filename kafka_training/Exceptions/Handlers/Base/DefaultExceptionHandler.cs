namespace kafka_training.Exceptions.Handlers.Base;

internal class DefaultExceptionHandler : BaseExceptionHandler<Exception>
{
    public override string ErrorCode => "UNHANDLED_EXCEPTION";
    public override string Message => "Unhandled exception";
}