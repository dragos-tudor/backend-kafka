
namespace Kafka.Instrumentation;

internal record ActivityTagNames
{
  public const string KafkaSystem = "kafka.system";
  public const string KafkaTopic = "kafka.messaging.topic";
  public const string KafkaPartition = "kafka.messaging.partition";
  public const string KafkaOffset = "kafka.messaging.offset";
  public const string MessageId = "message.id";
  public const string CorrelationId = "correlation.id";
  public const string Component = "component";
  public const string Error = "error";
}