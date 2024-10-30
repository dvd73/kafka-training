using Confluent.Kafka;

namespace kafka_training.Kafka;

public class KafkaConfiguration 
{
    public string Topic { get; set; }
    public string ClusterAddress { get; set; }

    public KafkaConfiguration()
    {
        Topic = "log-topic";
        ClusterAddress = "localhost:19092,localhost:29092,localhost:39092";
    }
}