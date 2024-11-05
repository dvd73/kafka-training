using DiceServiceApplication.Kafka;
using DiceServiceApplication.Services.Interfaces;
using DiceServiceApplication.Utils;

namespace DiceServiceApplication.Services;

public class LogAggregationService : IDisposable, IAggregationService
{
    private readonly KafkaConsumer<string, string> _consumer;
    private readonly CancellationTokenSource _cts = new();
    private readonly string _topic;

    public LogAggregationService(IKafkaConfiguration kafkaConfiguration, ConsumerFabric utils)
    {
        _topic = kafkaConfiguration.Topic;
        _consumer = utils.CreateLogConsumer("log");
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

    public void Dispose()
    {
        _consumer.Dispose();
        _cts.Dispose();
    }
}