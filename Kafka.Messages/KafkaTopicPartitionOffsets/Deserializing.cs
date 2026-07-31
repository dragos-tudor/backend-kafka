
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static TopicPartitionOffset? DeserializeTopicPartitionOffset(string? topicPartitionOffset) =>
    topicPartitionOffset?.Split('|') is string[] parts ?
      new TopicPartitionOffset(
        parts[0],
        int.Parse(parts[1], CultureInfo.InvariantCulture),
        long.Parse(parts[2], CultureInfo.InvariantCulture),
        !string.IsNullOrEmpty(parts[3]) ? int.Parse(parts[3], CultureInfo.InvariantCulture) : default
      ):
      default;
}