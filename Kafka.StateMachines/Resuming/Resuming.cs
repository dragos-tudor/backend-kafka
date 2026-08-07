using static Kafka.StateMachines.ResumingCounterType;

namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  internal static async Task<string?> ResumeInboxMessagesAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
    TServices services,
    CancellationToken ct = default)
  where TServices : IResumeInboxMessageServices<TKey, TValue, TPayload, TSession>
  where TData : IResumingStepData<TKey, TValue, TPayload>
  where TSession : IDisposable
  {
    IReadOnlyList<InboxMessage<TKey, TPayload>> messages;
    try
    {
      var batchSize = services.GetResumeBatchSize();
      var utcDate = services.GetUtcDate();
      messages = await services.GetInboxMessagesAsync(utcDate, batchSize, ct);
    }
    catch (Exception exception)
    {
      LogFetchInboxMessagesError(services.GetLogger(), exception);
      AddMetricCounter(services.GetMetricCounters<ResumingCounterType>(), ResumingCriticalErrorsCounter);
      return CriticalErrorResumeState;
    }

    var stateActions = GetResumingStateActions<TServices, TData, TKey, TValue, TPayload, TSession>();
    foreach (var message in messages)
    {
      using var activity = CreateComponentActivity(services.GetActivitySource(), "resume-inbox-message", ActivityKind.Internal);
      using var logScope = CreateComponentLogScope(services.GetLogger(), activity, "resume-inbox-message");

      var currentState = GetResumingEntryState(message.Status);
      var currentData = CreateResumingStepData<TKey, TValue, TPayload>(message);
      currentData.InboxMessage = message;

      try
      {
        await foreach (var (newData, newState) in RunStateMachineAsync(services, (TData)currentData, currentState, stateActions, ct))
        {
          currentData = newData;
          currentState = newState;
        }

        LogResumedInboxMessage(services.GetLogger(), currentState);
        AddMetricCounter(services.GetMetricCounters<ResumingCounterType>(), ResumedCounter);
      }
      catch (OperationCanceledException) { return default; }
    }
    return default;
  }
}