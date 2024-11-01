using DiceServiceApplication.Exceptions.Handlers.Base;

namespace DiceServiceApplication.Exceptions.Handlers.Custom;

internal class OperationCanceledExceptionHandler : BaseExceptionHandler<OperationCanceledException>
{
    public override string ErrorCode => "OPERATION_CANCELLED";
    public override string Message => "The operation was cancelled by user.";
}