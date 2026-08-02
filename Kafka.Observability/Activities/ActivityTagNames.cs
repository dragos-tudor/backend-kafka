
namespace Kafka.Observability;

internal record ActivityTagNames
{
  public const string KafkaSystem = "kafka.system";
  public const string KafkaTopic = "kafka.topic";
  public const string KafkaPartition = "kafka.partition";
  public const string KafkaOffset = "kafka.offset";
  public const string Component = "component";
  public const string MessageId = "message.id";
  public const string CorrelationId = "correlation.id";
  public const string Error = "error";
}