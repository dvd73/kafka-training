namespace DiceServiceApplication.Kafka;

public class KafkaHistoryConfiguration : IKafkaConfiguration
{
    public string Topic { get; set; }
    public string ClusterAddress { get; set; }

    public KafkaHistoryConfiguration()
    {
        Topic = "history-topic";
        ClusterAddress = "localhost:19092,localhost:29092,localhost:39092";
    }
}