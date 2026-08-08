
namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  internal static async ValueTask<string?> ConsumeKafkaMessagesAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
    TServices services,
    CancellationToken ct = default)
  where TServices : IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession>
  where TData: IConsumingStepData<TKey, TValue, TPayload>
  where TSession : IDisposable
  {
    var stateActions = GetConsumingStateActions<TServices, TData, TKey, TValue, TPayload, TSession>();
    while (!ct.IsCancellationRequested)
    {
      using var activity = CreateComponentActivity(services.GetActivitySource(), "consuming.kafka.message", ActivityKind.Consumer);
      using var logScope = CreateComponentLogScope(services.GetLogger(), activity, "consuming.kafka.message");

      var currentData = CreateConsumingStepData<TKey, TValue, TPayload>();
      var currentState = ConsumingNotStartedState;

      await foreach (var (newData, newState) in RunStateMachineAsync(services, (TData)currentData, currentState, stateActions, ct))
      {
        currentData = newData;
        currentState = newState;
      }
      if (ConsumingCriticalStates.Contains(currentState))
      {
        InstrumentConsumeKafkaMessageCriticalError(currentState, services);
        return ConsumingCriticalErrorState;
      }
      InstrumentConsumeKafkaMessage(currentState, services);
    }
    return default;
  }
}