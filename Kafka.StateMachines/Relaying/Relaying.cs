
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  internal static async Task<string?> RelayOutboxMessagesAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    CancellationToken ct = default)
  where TServices : IRelayOutboxMessagesServices<TKey, TValue, TPayload>
  where TData : IRelayingStepData<TKey, TValue, TPayload>
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

    var stateActions = GetRelayingStateActions<TServices, TData, TKey, TValue, TPayload>();
    foreach (var message in messages)
    {
      using var activity = CreateComponentActivity(services.GetActivitySource(), "relay.outbox.message", ActivityKind.Internal);
      using var logScope = CreateComponentLogScope(services.GetLogger(), activity, "relay.outbox.message");

      var currentState = GetRelayingEntryState(message.Status);
      var currentData = CreateRelayingStepData<TKey, TValue, TPayload>(message);
      currentData.OutboxMessage = message;

      await foreach (var (newData, newState) in RunStateMachineAsync(services, (TData)currentData, currentState, stateActions, ct))
      {
        currentData = newData;
        currentState = newState;
      }
      if (RelayingCriticalStates.Contains(currentState))
      {
        InstrumentRelayOutboxMessageCriticalError(currentState, services);
        return RelayingCriticalErrorState;
      }
      InstrumentRelayedOutboxMessage(message.MessageId, currentState, services);
    }
    return default;
  }
}