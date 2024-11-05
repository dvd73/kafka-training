using Confluent.Kafka;
using DiceServiceApplication.Domain.Models;
using DiceServiceApplication.Kafka;
using DiceServiceApplication.Kafka.Serialization;

namespace DiceServiceApplication.Utils;

public class ProducerFabric
{
    private readonly HistoryEntrySerializer _historyEntrySerializer;
    private readonly IKafkaConfiguration _kafkaConfiguration;

    public ProducerFabric(IKafkaConfiguration kafkaConfiguration, HistoryEntrySerializer historyEntrySerializer)
    {
        _historyEntrySerializer = historyEntrySerializer;
        _kafkaConfiguration = kafkaConfiguration;
    }

    private ProducerConfig GetGeneralProducerConfig()
    {
        return new ProducerConfig
        {
            BootstrapServers = _kafkaConfiguration.ClusterAddress
        };
    }

    public KafkaProducer<string, string> CreateLogProducer()
    {
        var config = GetGeneralProducerConfig();

        return new KafkaProducer<string, string>(config,
            Serializers.Utf8, Serializers.Utf8);
    }

    public KafkaProducer<string, HistoryEntry> CreateHistoryProducer()
    {
        var config = GetGeneralProducerConfig();

        return new KafkaProducer<string, HistoryEntry>(config,
            Serializers.Utf8, _historyEntrySerializer);
    }
}