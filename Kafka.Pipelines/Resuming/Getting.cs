
namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static async Task<IReadOnlyList<InboxMessage<TKey, TPayload>>?> GetResumingInboxMessagesAsync<TServices, TKey, TPayload, TSession>(
    TServices services,
    CancellationToken ct = default)
  where TServices : IResumingServices<TKey, TPayload, TSession>
  where TSession : IDisposable
  {
    try
    {
      var batchSize = services.GetResumeBatchSize();
      var utcDate = services.GetUtcDate();
      return await services.GetInboxMessagesAsync(utcDate, batchSize, ct);
    }
    catch(OperationCanceledException) { return default!; }
    catch(Exception exception)
    {
      InstrumentFetchInboxMessageError(exception, services);
      return default;
    }
  }
}