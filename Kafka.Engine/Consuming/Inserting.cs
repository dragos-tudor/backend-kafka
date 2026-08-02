
namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static async Task<InboxMessage<TKey, TPayload>?> InsertInboxMessageAsync<TKey, TValue, TPayload>(
    ConsumeResult<TKey, TValue> result,
    IInsertInboxMessage<TKey, TValue, TPayload> services,
    CancellationToken cancellationToken)
  {
    var message = ToInboxMessage(result.Message, result.TopicPartitionOffset, services.ToPersistedMessagePayload, services.GetUtcDate());
    var inserted = await services.InsertInboxMessageAsync(message, cancellationToken);
    return inserted ? message : null;
  }
}