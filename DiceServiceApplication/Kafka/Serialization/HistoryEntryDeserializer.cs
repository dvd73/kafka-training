using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Confluent.Kafka;
using DiceServiceApplication.Domain.Models;
using Microsoft.Extensions.Logging;

namespace DiceServiceApplication.Kafka.Serialization;

public class HistoryEntryDeserializer(ILogger<HistoryEntryDeserializer> logger) : IDeserializer<HistoryEntry>
{
    private readonly ILogger<HistoryEntryDeserializer> _logger = logger;

    [Obsolete("Obsolete")]
    public HistoryEntry Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        using var inputStream = new MemoryStream(data.ToArray());
        var formatter = new BinaryFormatter();
        try
        {
            return (HistoryEntry)formatter.Deserialize(inputStream);
        }
        catch (Exception e) when (e is IOException or SerializationException)
        {
            logger.LogError(e, "Deserialize error:");
        }

        return null;
    }
}