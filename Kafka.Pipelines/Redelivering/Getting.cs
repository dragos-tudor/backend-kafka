
namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  internal static async Task<IReadOnlyList<DeadLetterMessage<TKey, TPayload>>?> GetRedeliveringDeadLetterMessagesAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    CancellationToken ct = default)
  where TServices : IRedeliveringServices<TKey, TValue, TPayload>
  where TData : IRedeliveringData<TKey, TValue, TPayload>
  {
    try
    {
      var batchSize = services.GetRelayBatchSize();
      var utcDate = services.GetUtcDate();
      return await services.GetDeadLetterMessagesAsync(utcDate, batchSize, ct);
    }
    catch(OperationCanceledException) { return default; }
    catch(Exception exception)
    {
      InstrumentFetchOutboxMessageError(exception, services);
      return default;
    }
  }
}