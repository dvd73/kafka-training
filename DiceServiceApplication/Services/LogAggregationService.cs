using System;
using System.Collections.Generic;
using System.Threading;
using Confluent.Kafka;
using DiceServiceApplication.Kafka;
using DiceServiceApplication.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace DiceServiceApplication.Services;

public class LogAggregationService : IDisposable
{
    private readonly KafkaConsumer<string, string> _consumer;
    private readonly string _topic;
    private readonly CancellationTokenSource _cts = new();

    public LogAggregationService(KafkaConfiguration kafkaConfiguration, ConsumerUtils utils)
    {
        _topic = kafkaConfiguration.Topic;
        _consumer = utils.CreateConsumer("log");
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
    }

    public void Dispose()
    {
        _consumer.Dispose();
        _cts.Dispose();
    }
}

