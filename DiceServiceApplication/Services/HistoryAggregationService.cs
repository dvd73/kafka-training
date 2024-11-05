using System;
using System.Collections.Generic;
using System.Threading;
using Confluent.Kafka;
using DiceServiceApplication.Domain.Models;
using DiceServiceApplication.Kafka;
using DiceServiceApplication.Kafka.Serialization;
using DiceServiceApplication.Services.Interfaces;
using DiceServiceApplication.Utils;

namespace DiceServiceApplication.Services;

public class HistoryAggregationService: IDisposable, IAggregationService
{
    private readonly KafkaConsumer<string, HistoryEntry> _consumer;
    private readonly string _topic;
    private readonly CancellationTokenSource _cts = new();
    
    public HistoryAggregationService(IKafkaConfiguration kafkaConfiguration, ConsumerFabric utils)
    {
        _topic = kafkaConfiguration.Topic;
        _consumer = utils.CreateHistoryConsumer("history");
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
    /*
    public void Listen()
    {
        var currentOffsets = new Dictionary<TopicPartition, OffsetAndMetadata>();

        try
        {
            var topics = new List<string> { KafkaConfiguration.TOPIC_HISTORY };
            var rebalanceListener = new HistoryRebalanceListener(consumer, currentOffsets);
            consumer.Subscribe(topics, rebalanceListener);

            while (true)
            {
                var records = consumer.Consume(TimeSpan.FromMilliseconds(300));
                foreach (var record in records)
                {
                    var sessionId = record.Key;
                    var playerId = record.Value.GetPlayerId();
                    var result = record.Value.GetRollResult();
                    var message = string.Format("[Session: {0}] Player: {1}, Result: {2}", sessionId, playerId, result);
                    Console.WriteLine(message);

                    var partition = new TopicPartition(record.Topic, record.Partition);
                    var offsetAndMetadata = new OffsetAndMetadata(record.Offset + 1, "No META");
                    currentOffsets[partition] = offsetAndMetadata;
                }
                consumer.CommitAsync(currentOffsets);
            }
        }
        catch (ConsumeException e)
        {
            // ignored
        }
        catch (Exception e)
        {
            var errorMessage = string.Format("Listen log error: {0}", e.Message);
            Console.WriteLine(errorMessage);
        }
        finally
        {
            consumer.Close();
        }
    }*/
}

