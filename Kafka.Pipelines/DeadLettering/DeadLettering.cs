using static Kafka.Pipelines.DeadLetteringStates;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  internal static async Task<string?> DeadLetterInboxMessagesAsync<TServices, TData, TKey, TPayload>(
    TServices services,
    CancellationToken ct = default)
  where TServices : IDeadLetteringServices<TKey, TPayload>
  where TData : IDeadLetteringData<TKey, TPayload>
  {
    var messages = await GetDeadLetteringInboxMessagesAsync<TServices, TKey, TPayload>(services, ct);
    if (messages is null) return default;

    foreach (var message in messages)
    {
      using var activity = CreateDefaultActivity(services.GetActivitySource(), "deadletter.inbox.message", ActivityKind.Internal);
      using var logScope = CreateComponentLogScope(services.GetLogger(), activity, "deadletter.inbox.message");

      var currentData = (TData)CreateDeadLetteringData(message);
      var getStateAction = GetDeadLetteringStateAction<TServices, TData, TKey, TPayload>;
      var initialState = DeadLetteringNotStartedState;

      var (_, lastState) = await RunStateMachineAsync(services, currentData, initialState, getStateAction, ct);
      InstrumentDeadLetteredInboxMessage(message.MessageId, lastState, services);
    }
    return default;
  }
}
