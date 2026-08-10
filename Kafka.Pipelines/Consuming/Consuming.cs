using static Kafka.Pipelines.ConsumingStates;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  internal static async ValueTask<string?> ConsumeKafkaMessagesAsync<TServices, TData, TKey, TValue, TPayload, TSession>(
    TServices services,
    CancellationToken ct = default)
  where TSession : IDisposable
  where TServices : IConsumingServices<TKey, TValue, TPayload, TSession>
  where TData : IConsumingData<TKey, TValue, TPayload>
  {
    while (!ct.IsCancellationRequested)
    {
      using var activity = CreateComponentActivity(services.GetActivitySource(), "consuming.kafka.message", ActivityKind.Consumer);
      using var logScope = CreateComponentLogScope(services.GetLogger(), activity, "consuming.kafka.message");

      var currentData = (TData)CreateConsumingData<TKey, TValue, TPayload>(PipelineType.Consuming);
      var currentState = ConsumingNotStartedState;
      var getStateAction = GetConsumingStateAction<TServices, TData, TKey, TValue, TPayload, TSession>;

      await foreach (var (newData, newState) in RunStateMachineAsync(services, currentData, currentState, getStateAction, ct))
      {
        if (ConsumingCriticalStates.Contains(newState))
        {
          InstrumentConsumeKafkaMessageCriticalError(newState, services);
          return ConsumingCriticalErrorState;
        }
        currentData = newData;
        currentState = newState;
      }
      InstrumentConsumeKafkaMessage(currentState, services);
    }
    return default;
  }
}