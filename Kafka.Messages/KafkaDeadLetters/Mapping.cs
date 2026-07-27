
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static Message<TKey, TValue> ToKafkaDeadLetter<TKey, TValue, TPayload>(
    MessageBase<TKey, TPayload> message,
    string failureReason,
    DateTime date,
    Func<TPayload, TValue> mapper)
  {
    var headers = SetKafkaMessageHeaders([], message.MessageId, message.Type, message.Version, message.CorrelationId);
    var kafkaMessage = CreateKafkaMessage(message.MessageKey, ToMessageValue(message, mapper)!, headers, date);
    var topicPartitionOffset = DeserializeTopicPartitionOffset(message.Metadata);

    return CreateKafkaDeadLetter(kafkaMessage, topicPartitionOffset, failureReason);
  }
}