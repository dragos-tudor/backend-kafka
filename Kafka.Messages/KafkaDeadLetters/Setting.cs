
namespace Kafka.Messages;

partial class MessagesFuncs
{
  static Headers SetDeadLetterHeaders<TKey, TValue>(
    Message<TKey, TValue> message,
    TopicPartitionOffset? topicPartitionOffset,
    string failureReason)
  {
    Headers headers = CloneKafkaHeaders(message.Headers);
    SetKafkaHeaderString(headers, DeadLetterReasonHeaderName, failureReason);
    SetKafkaHeaderString(headers, OriginalOffsetHeaderName, topicPartitionOffset?.Offset.Value.ToString(CultureInfo.InvariantCulture));
    SetKafkaHeaderString(headers, OriginalPartitionHeaderName, topicPartitionOffset?.Partition.Value.ToString(CultureInfo.InvariantCulture));
    SetKafkaHeaderString(headers, OriginalTopicHeaderName, topicPartitionOffset?.Topic);
    return headers;
  }
}