
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static bool IsValidOutboxMessage<TKey, TPayload>(OutboxMessage<TKey, TPayload> message) =>
    IsValidMessageId(message.MessageId) &&
    IsValidMessageKey(message.MessageKey) &&
    IsValidMessageType(message.Type) &&
    IsValidMessageMetadata(message.Metadata) &&
    IsValidMessageLastError(message.LastError);
}