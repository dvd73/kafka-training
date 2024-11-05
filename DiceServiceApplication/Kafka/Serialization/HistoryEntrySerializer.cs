using System.Runtime.Serialization.Formatters.Binary;
using Confluent.Kafka;
using DiceServiceApplication.Domain.Models;

namespace DiceServiceApplication.Kafka.Serialization;

public class HistoryEntrySerializer : ISerializer<HistoryEntry>
{
    [Obsolete("Obsolete")]
    public byte[] Serialize(HistoryEntry data, SerializationContext context)
    {
        if (data == null)
        {
            return null;
        }

        using var resultStream = new MemoryStream();
        var binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(resultStream, data);
        return resultStream.ToArray();
    }
}

