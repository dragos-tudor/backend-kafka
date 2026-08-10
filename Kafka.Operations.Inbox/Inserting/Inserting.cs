using static Kafka.Operations.Inbox.InsertingStates;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal static async ValueTask<(TData, string)> InsertInboxMessageAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IInsertingServices<TKey, TValue, TPayload>
  where TData : IInsertingData<TKey, TValue, TPayload>
  {
    var messageKey = data.KafkaMessage!.Key;
    try {
      var kafkaMessage = data.KafkaMessage!;
      var topicPartitionOffset = data.TopicPartitionOffset!;
      var status = InboxMessageStatus.Pending;

      var inboxMessage = ToInboxMessage(kafkaMessage, topicPartitionOffset, services.ToIntegrationPayload, services.GetUtcDate(), status);
      var inboxMessageInserted = await services.InsertInboxMessageAsync(inboxMessage, ct);
      if (!inboxMessageInserted)
      {
        InstrumentIdempotentInboxMessage(inboxMessage.MessageId, services);
        return (data, IdempotentInboxMessageState);
      }

      data.InboxMessage = inboxMessage;
      InstrumentInsertedInboxMessage(inboxMessage.MessageId, services);
      return (data, InsertedInboxMessageState);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentInsertInboxMessageError(messageKey?.ToString(), ex, services);
      return (data, InsertInboxMessageErrorState);
    }
  }
}