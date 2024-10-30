using Confluent.Kafka;
using kafka_training.Kafka;
using Microsoft.Extensions.DependencyInjection;

namespace kafka_training.Utils;

public class ProducerUtils
{
    private readonly KafkaConfiguration _kafkaConfiguration;

    public ProducerUtils(KafkaConfiguration kafkaConfiguration)
    {
        _kafkaConfiguration = kafkaConfiguration;
    }
    
    public KafkaProducer<string, string> CreateProducer()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = _kafkaConfiguration.ClusterAddress
        };

        return new KafkaProducer<string, string>(config,
            Serializers.Utf8, Serializers.Utf8);
    }
}