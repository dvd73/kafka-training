using DiceServiceApplication.Kafka;
using DiceServiceApplication.Utils;

namespace DiceServiceApplication.Services;

public class LogService : IDisposable
{
    private readonly KafkaProducer<string, string> _producer;
    private readonly string _topic;

    public LogService(KafkaLogConfiguration kafkaConfiguration, ProducerFabric utils)
    {
        _producer = utils.CreateLogProducer();
        _topic = kafkaConfiguration.Topic;
    }

    public void LogMessage(string message)
    {
        _producer.ProduceAsync(_topic, null, message);
    }

    public void Dispose()
    {
        _producer.Dispose();
    }
}