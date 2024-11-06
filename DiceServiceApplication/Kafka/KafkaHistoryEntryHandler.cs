using Confluent.Kafka;
using DiceServiceApplication.Domain.Models;
using Microsoft.Extensions.Logging;

namespace DiceServiceApplication.Kafka;

public class KafkaHistoryEntryHandler : KafkaBaseMessageHandler<string, HistoryEntry>
{
    public KafkaHistoryEntryHandler(ILogger<KafkaBaseMessageHandler<string, HistoryEntry>> logger) : base(logger)
    {
    }
    
    public override async Task HandleMessageAsync(ConsumeResult<string, HistoryEntry> consumeResult)
    {
        var sessionId = consumeResult.Message.Key;
        var result = consumeResult.Message.Value.RollResult;
        var playerId = consumeResult.Message.Value.PlayerId;
        var message = $"[Session: {sessionId}] Player: {playerId}, Result: {result}";
        Logger.LogInformation(message);
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