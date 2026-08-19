using static Kafka.Pipelines.RelayingStates;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  internal static async Task<string?> RelayOutboxMessagesAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    CancellationToken ct = default)
  where TServices : IRelayingServices<TKey, TValue, TPayload>
  where TData : IRelayingData<TKey, TValue, TPayload>
  {
    var messages = await GetRelayingOutboxMessagesAsync<TServices, TData, TKey, TValue, TPayload>(services, ct);
    if (messages is null) return default;

    foreach (var message in messages)
    {
      using var activity = CreateDefaultActivity(services.GetActivitySource(), "relay.outbox.message", ActivityKind.Internal);
      using var logScope = CreateComponentLogScope(services.GetLogger(), activity, "relay.outbox.message");

      var initialData = (TData)CreateRelayingData<TKey, TValue, TPayload>(message);
      var initialState = RelayingNotStartedState;
      var getStateAction = GetRelayingStateAction<TServices, TData, TKey, TValue, TPayload>;

      var (_, lastState) = await RunStateMachineAsync(services, initialData, initialState, getStateAction, ct);
      if (RelayingCriticalStates.Contains(lastState))
      {
        InstrumentRelayOutboxMessageCriticalError(lastState, services);
        return RelayingCriticalErrorState;
      }
      InstrumentRelayedOutboxMessage(message.MessageId, lastState, services);
    }
    return default;
  }
}