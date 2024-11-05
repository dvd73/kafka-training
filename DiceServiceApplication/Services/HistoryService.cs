using Confluent.Kafka;
using DiceServiceApplication.Domain.Models;
using DiceServiceApplication.Kafka;
using DiceServiceApplication.Utils;
using Microsoft.Extensions.Logging;

namespace DiceServiceApplication.Services;

public class HistoryService
{
    private readonly ILogger<HistoryService> _logger;
    private readonly KafkaProducer<string, HistoryEntry> _producer;
    private readonly string _topic;

    public HistoryService(IKafkaConfiguration kafkaConfiguration, ProducerFabric utils, ILogger<HistoryService> logger)
    {
        _logger = logger;
        _producer = utils.CreateHistoryProducer();
        _topic = kafkaConfiguration.Topic;
    }

    public void LogRoll(string sessionId, string playerId, int rollResult)
    {
        try
        {
            var entry = new HistoryEntry(playerId, rollResult);
            _producer.Produce(_topic, sessionId, entry, historyCallback);

            void historyCallback(DeliveryReport<string, HistoryEntry> obj)
            {
                if (obj.Error != null && obj.Error.IsError) _logger.LogError(obj.Error.ToString());
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Log roll error:");
            throw;
        }
    }
}