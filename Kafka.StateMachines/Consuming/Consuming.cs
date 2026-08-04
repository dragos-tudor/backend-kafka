
using static Kafka.Operations.OperationState;
using static Kafka.Operations.MetricCounterType;

namespace Kafka.StateMachines;

partial class StateMachinesFuncs
{
  internal static async ValueTask<ConsumingError> ConsumeKafkaMessagesAsync<TKey, TValue, TPayload, TSession>(
    IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession> services,
    CancellationToken ct = default)
  where TSession : IDisposable
  {
    var stateActions = GetConsumingStateActions<TKey, TValue, TPayload, TSession>();
    while (!ct.IsCancellationRequested)
    {
      using var activity = CreateComponentActivity(services.GetActivitySource(), "consume-kafka-message", ActivityKind.Consumer);
      using var logScope = CreateComponentLogScope(services.GetLogger(), activity, "consume-kafka-message");

      var currentData = CreateConsumingStepData<TKey, TValue, TPayload>();
      var currentState = NotStartedState;
      try
      {
        await foreach (var (newData, newState) in RunStateMachineAsync(services, currentData, currentState, stateActions, ct))
        {
          currentData = newData;
          currentState = newState;
        }

        LogConsumedKafkaMessage(services.GetLogger(), currentState);
      }
      catch (OperationCanceledException) { return ConsumingError.None; }
      catch (Exception exception)
      {
        LogConsumingKafkaMessageFailed(services.GetLogger(), exception, currentState);
        AddMetricCounter(services.GetMetricCounters(), ConsumingErrorsCounter);

        var error = ToConsumingError(currentState);
        if (error != ConsumingError.None)
          return error;
      }
    }
    return ConsumingError.None;
  }
}