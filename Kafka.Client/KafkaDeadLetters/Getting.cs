
namespace Kafka.Client;

partial class KafkaFuncs
{
  public static string GetDeadLetterTopicName(string topicName, string suffix = "-dlq") => $"{topicName}{suffix}";
}