using static Kafka.Operations.OperationState;

namespace Kafka.Operations;

partial class OperationsFuncs
{
  internal static async ValueTask<(TData, OperationState)> InsertInboxMessageAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IInsertInboxMessageServices<TKey, TValue, TPayload>
  where TData : IInsertInboxMessageData<TKey, TValue, TPayload>
  {
    var kafkaMessage = data.KafkaMessage!;
    var topicPartitionOffset = data.TopicPartitionOffset!;

    var inboxMessage = ToInboxMessage(kafkaMessage, topicPartitionOffset, services.ToInboxMessagePayload, services.GetUtcDate());
    var messageSaved = await services.InsertInboxMessageAsync(inboxMessage, ct);
    if (messageSaved)
    {
      InstrumentIdempotentInboxMessage(services);
      return (data, IdempotentInboxMessageState);
    }

    data.InboxMessage = inboxMessage;
    InstrumentInsertInboxMessage(services);
    return (data, InsertedInboxMessageState);
  }
}