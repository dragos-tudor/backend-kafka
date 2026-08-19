namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  static async Task<IReadOnlyList<InboxMessage<TKey, TPayload>>?> GetDeadLetteringInboxMessagesAsync<TServices, TKey, TPayload>(
    TServices services,
    CancellationToken ct = default)
  where TServices : IDeadLetteringServices<TKey, TPayload>
  {
    try
    {
      var batchSize = services.GetDeadLetteringBatchSize();
      var utcDate = services.GetUtcDate();
      return await services.GetDeadLetteringInboxMessagesAsync(utcDate, batchSize, ct);
    }
    catch(OperationCanceledException) { return default!; }
    catch(Exception exception)
    {
      InstrumentFetchDeadLetteringInboxMessageError(exception, services);
      return default;
    }
  }
}
