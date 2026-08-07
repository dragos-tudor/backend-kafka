
namespace Kafka.Inbox;

partial class InboxFuncs
{
  internal static async ValueTask<(TData, string)> InsertInboxMessageAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IInsertInboxMessageServices<TKey, TValue, TPayload>
  where TData : IInsertInboxMessageData<TKey, TValue, TPayload>
  {
    var messageKey = data.KafkaMessage!.Key;
    try {
      var kafkaMessage = data.KafkaMessage!;
      var topicPartitionOffset = data.TopicPartitionOffset!;
      var status = InboxMessageStatus.Pending;

      var inboxMessage = ToInboxMessage(kafkaMessage, topicPartitionOffset, services.ToIntegrationPayload, services.GetUtcDate(), status);
      var messageSaved = await services.InsertInboxMessageAsync(inboxMessage, ct);
      if (messageSaved)
      {
        InstrumentIdempotentInboxMessage(inboxMessage.MessageId, services);
        return (data, IdempotentInboxMessageState);
      }

      data.InboxMessage = inboxMessage;
      InstrumentInsertedInboxMessage(inboxMessage.MessageId, services);
      return (data, InsertedInboxMessageState);
    }
    catch (Exception ex) {
      InstrumentInsertInboxMessageError(messageKey?.ToString(), ex, services);
      return (data, InsertInboxMessageErrorState);
    }
  }
}