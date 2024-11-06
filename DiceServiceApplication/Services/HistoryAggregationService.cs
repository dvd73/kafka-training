using DiceServiceApplication.Domain.Models;
using DiceServiceApplication.Kafka;
using DiceServiceApplication.Utils;

namespace DiceServiceApplication.Services;

public class HistoryAggregationService : IDisposable
{
    private readonly KafkaConsumer<string, HistoryEntry> _consumer;
    private readonly CancellationTokenSource _cts = new();
    private readonly string _topic;

    public HistoryAggregationService(KafkaHistoryConfiguration kafkaConfiguration, ConsumerFabric utils)
    {
        _topic = kafkaConfiguration.Topic;
        _consumer = utils.CreateHistoryConsumer("history");
    }

    public void Dispose()
    {
        _consumer.Dispose();
        _cts.Dispose();
    }

    public async void Run()
    {
        var cancellationToken = _cts.Token;
        await _consumer.Consume(_topic, cancellationToken);
    }

    public void Close()
    {
        if (!_cts.IsCancellationRequested) _cts.Cancel();
        _consumer.Close();
        Dispose();
    }
}