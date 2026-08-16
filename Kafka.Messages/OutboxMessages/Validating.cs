using static Kafka.Messages.MessageFieldConstraints;

namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static IEnumerable<string> ValidateOutboxMessage<TKey, TPayload>(OutboxMessage<TKey, TPayload> message)
  {
    if (!IsValidMessageId(message.MessageId)) yield return "MessageId is empty.";
    if (!IsValidMessageKey(message.MessageKey)) yield return "MessageKey is null.";
    if (!IsValidMessageType(message.Type)) yield return $"Type exceeds max length of {TypeMaxLength} (was {message.Type?.Length}).";
    if (!IsValidMessageMetadata(message.Metadata)) yield return $"Metadata exceeds max length of {MetadataMaxLength} (was {message.Metadata?.Length}).";
    if (!IsValidMessageLastError(message.LastError)) yield return $"LastError exceeds max length of {LastErrorMaxLength} (was {message.LastError?.Length}).";
  }
}