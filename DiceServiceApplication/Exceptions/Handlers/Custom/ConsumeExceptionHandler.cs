using Confluent.Kafka;
using DiceServiceApplication.Exceptions.Handlers.Base;

namespace DiceServiceApplication.Exceptions.Handlers.Custom;

internal class ConsumeExceptionHandler: BaseExceptionHandler<ConsumeException>
{
    public override string ErrorCode => "CONSUMPTION_ERROR";
    public override string Message => "Consume error";
}
