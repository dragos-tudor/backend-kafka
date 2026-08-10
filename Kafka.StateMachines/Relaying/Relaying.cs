using static Kafka.StateMachines.RelayingStates;

namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  internal static async Task<string?> RelayOutboxMessagesAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    CancellationToken ct = default)
  where TServices : IRelayOutboxMessagesServices<TKey, TValue, TPayload>
  where TData : IRelayingData<TKey, TValue, TPayload>
  {
    IReadOnlyList<OutboxMessage<TKey, TPayload>> messages;
    try
    {
      var batchSize = services.GetRelayBatchSize();
      var utcDate = services.GetUtcDate();
      messages = await services.GetOutboxMessagesAsync(utcDate, batchSize, ct);
    }
    catch(OperationCanceledException) { return default; }
    catch(Exception exception)
    {
      InstrumentFetchOutboxMessageError(exception, services);
      return RelayingCriticalErrorState;
    }

    foreach (var message in messages)
    {
      using var activity = CreateComponentActivity(services.GetActivitySource(), "relay.outbox.message", ActivityKind.Internal);
      using var logScope = CreateComponentLogScope(services.GetLogger(), activity, "relay.outbox.message");

      var currentData = (TData)CreateRelayingData<TKey, TValue, TPayload>(message);
      var currentState = GetRelayingEntryState(message.Status);
      var stateAction = GetRelayingStateAction<TServices, TData, TKey, TValue, TPayload>;

      await foreach (var (newData, newState) in RunStateMachineAsync(services, currentData, currentState, stateAction, ct))
      {
        if (RelayingCriticalStates.Contains(newState))
        {
          InstrumentRelayOutboxMessageCriticalError(newState, services);
          return RelayingCriticalErrorState;
        }
        currentData = newData;
        currentState = newState;
      }
      InstrumentRelayedOutboxMessage(message.MessageId, currentState, services);
    }
    return default;
  }
}