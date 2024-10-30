using kafka_training.Kafka;
using kafka_training.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace kafka_training.Services;

public class LogService
{
    private readonly KafkaProducer<string, string> _producer;
    private readonly string _topic;

    public LogService(KafkaConfiguration kafkaConfiguration, ProducerUtils utils)
    {
        _producer = utils.CreateProducer();
        _topic = kafkaConfiguration.Topic;
    }

    public void Log(string message)
    {
        try
        {
            _producer.ProduceAsync(_topic, null, message);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Cannot send message: {message}; Reason: {e.Message}");
        }
    }
}