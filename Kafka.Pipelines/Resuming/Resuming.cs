using static Kafka.Pipelines.ResumingStates;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  internal static async Task<string?> ResumeInboxMessagesAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
    TServices services,
    CancellationToken ct = default)
  where TServices : IResumingServices<TKey, TValue, TPayload, TSession>
  where TData : IResumingData<TKey, TValue, TPayload>
  where TSession : IDisposable
  {
    IReadOnlyList<InboxMessage<TKey, TPayload>> messages;
    try
    {
      var batchSize = services.GetResumeBatchSize();
      var utcDate = services.GetUtcDate();
      messages = await services.GetInboxMessagesAsync(utcDate, batchSize, ct);
    }
    catch(OperationCanceledException) { return default; }
    catch(Exception exception)
    {
      InstrumentFetchInboxMessageError(exception, services);
      return ResumingCriticalErrorState;
    }

    foreach (var message in messages)
    {
      using var activity = CreateComponentActivity(services.GetActivitySource(), "resume.inbox.message", ActivityKind.Internal);
      using var logScope = CreateComponentLogScope(services.GetLogger(), activity, "resume.inbox.message");

      var currentData = (TData)CreateResumingData<TKey, TValue, TPayload>(message, PipelineType.Resuming);
      var currentState = GetResumingEntryState(message.Status);
      var stateAction = GetResumingStateAction<TServices, TData, TKey, TValue, TPayload, TSession>;

      await foreach (var (newData, newState) in RunStateMachineAsync(services, currentData, currentState, stateAction, ct))
      {
        if (ResumingCriticalStates.Contains(newState))
        {
          InstrumentResumeInboxMessageCriticalError(newState, services);
          return ResumingCriticalErrorState;
        }
        currentData = newData;
        currentState = newState;
      }
      InstrumentResumedInboxMessage(message.MessageId, currentState, services);
    }
    return default;
  }
}