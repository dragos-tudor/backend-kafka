
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
      using var activity = CreateComponentActivity(services.GetActivitySource(), "consuming-kafka-message", ActivityKind.Consumer);
      using var logScope = CreateComponentLogScope(services.GetLogger(), activity, "consuming-kafka-message");

      var currentData = CreateConsumingStepData<TKey, TValue, TPayload>();
      var currentState = NotStartedConsumeState;
      try
      {
        await foreach (var (newData, newState) in RunStateMachineAsync(services, (TData)currentData, currentState, stateActions, ct))
        {
          if (ConsumingCriticalStates.Contains(newState))
          {
            InstrumentConsumeKafkaMessageCriticalError(newState, services);
            return CriticalErrorConsumeState;
          }
          currentData = newData;
          currentState = newState;
        }
        InstrumentConsumeKafkaMessage(currentState, services);
      }
      catch (OperationCanceledException) { return default; }
    }
    return default;
  }
}