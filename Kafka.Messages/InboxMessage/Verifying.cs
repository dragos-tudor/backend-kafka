
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static bool IsValidInboxMessage<TKey, TPayload>(InboxMessage<TKey, TPayload> message) =>
    IsValidMessageId(message.MessageId) &&
    IsValidMessageKey(message.MessageKey) &&
    IsValidMessageType(message.Type) &&
    IsValidMessageMetadata(message.Metadata) &&
    IsValidMessageLastError(message.LastError);
}