using Confluent.Kafka;
using kafka_training.Exceptions.Handlers.Base;

namespace kafka_training.Exceptions.Handlers.Custom;

internal class ConsumeExceptionHandler: BaseExceptionHandler<ConsumeException>
{
    public override string ErrorCode => "CONSUMPTION_ERROR";
    public override string Message => "Some error happened during message consumption.";
}
