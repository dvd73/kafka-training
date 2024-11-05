using DiceServiceApplication.Kafka;
using DiceServiceApplication.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiceServiceApplication.Services;

public class LogService : IDisposable
{
    private readonly KafkaProducer<string, string> _producer;
    private readonly string _topic;

    public LogService(IKafkaConfiguration kafkaConfiguration, ProducerFabric utils)
    {
        _producer = utils.CreateLogProducer();
        _topic = kafkaConfiguration.Topic;
    }

    public void Log(string message)
    {
        _producer.ProduceAsync(_topic, null, message);
    }

    public void Dispose()
    {
        _producer.Dispose();
    }
}