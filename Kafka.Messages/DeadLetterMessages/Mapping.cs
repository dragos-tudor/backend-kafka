
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static DeadLetterMessage<TKey, TPayload> ToDeadLetterMessage<TKey, TPayload>(
    InboxMessage<TKey, TPayload> inboxMessage,
    string failureReason,
    DateTime createdAt) =>
    new (){
      MessageId = inboxMessage.MessageId,
      MessageKey = inboxMessage.MessageKey,
      Payload = inboxMessage.Payload,
      Date = inboxMessage.Date,
      Status = DeadLetterMessageStatus.Pending,
      ReceivedAt = inboxMessage.ReceivedAt,
      CreatedAt = createdAt,
      Type = inboxMessage.Type,
      Version = inboxMessage.Version,
      Metadata = inboxMessage.Metadata,
      CorrelationId = inboxMessage.CorrelationId,
      FailureReason = TruncateDeadLetterMessageFailureReason(failureReason)
    };
}