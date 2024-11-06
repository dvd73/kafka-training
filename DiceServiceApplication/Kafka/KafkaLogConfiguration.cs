using Confluent.Kafka;

namespace DiceServiceApplication.Kafka;

public class KafkaLogConfiguration 
{
    public string Topic { get; set; }
    public string ClusterAddress { get; set; }

    public KafkaLogConfiguration()
    {
        Topic = "log-topic";
        ClusterAddress = "localhost:19092,localhost:29092,localhost:39092";
    }
}