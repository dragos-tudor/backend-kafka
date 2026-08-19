using static Kafka.Pipelines.ResumingStates;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  internal static async Task<string?> ResumeInboxMessagesAsync<TServices, TData, TKey, TPayload, TSession>(
    TServices services,
    CancellationToken ct = default)
  where TServices : IResumingServices<TKey, TPayload, TSession>
  where TData : IResumingData<TKey, TPayload>
  where TSession : IDisposable
  {
    var messages = await GetResumingInboxMessagesAsync<TServices, TKey, TPayload, TSession>(services, ct);
    if (messages is null) return default;

    foreach (var message in messages)
    {
      using var activity = CreateDefaultActivity(services.GetActivitySource(), "resume.inbox.message", ActivityKind.Internal);
      using var logScope = CreateComponentLogScope(services.GetLogger(), activity, "resume.inbox.message");

      var initialData = (TData)CreateResumingData(message);
      var initialState = ResumingNotStartedState;
      var getStateAction = GetResumingStateAction<TServices, TData, TKey, TPayload, TSession>;

      var (_, lastState) = await RunStateMachineAsync(services, initialData, initialState, getStateAction, ct);
      InstrumentResumedInboxMessage(message.MessageId, lastState, services);
    }
    return default;
  }
}