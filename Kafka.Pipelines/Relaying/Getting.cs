
namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  internal static async Task<IReadOnlyList<OutboxMessage<TKey, TPayload>>?> GetRelayingOutboxMessagesAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    CancellationToken ct = default)
  where TServices : IRelayingServices<TKey, TValue, TPayload>
  where TData : IRelayingData<TKey, TValue, TPayload>
  {
    try
    {
      var batchSize = services.GetRelayBatchSize();
      var utcDate = services.GetUtcDate();
      return await services.GetOutboxMessagesAsync(utcDate, batchSize, ct);
    }
    catch(OperationCanceledException) { return default; }
    catch(Exception exception)
    {
      InstrumentFetchOutboxMessageError(exception, services);
      return default;
    }
  }
}