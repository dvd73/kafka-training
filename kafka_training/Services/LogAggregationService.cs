using System;
using System.Collections.Generic;
using System.Threading;
using Confluent.Kafka;
using kafka_training.Kafka;
using kafka_training.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace kafka_training.Services;

public class LogAggregationService
{
    private readonly KafkaConsumer<string, string> _consumer;
    private readonly string _topic;
    private readonly CancellationTokenSource _cts = new();

    public LogAggregationService(KafkaConfiguration kafkaConfiguration, ConsumerUtils utils)
    {
        _consumer = utils.CreateConsumer("log");
        _topic = kafkaConfiguration.Topic;
    }
    
    public async void Run()
    {
        var cancellationToken = _cts.Token;
        await _consumer.Consume(_topic, cancellationToken);
    }

    public void Close()
    {
        _cts.Cancel();
        _consumer.Close();
    }
}

