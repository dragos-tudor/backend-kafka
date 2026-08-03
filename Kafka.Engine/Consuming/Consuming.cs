
using static Kafka.Engine.ConsumingState;

namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static async Task<ConsumingError> ConsumeKafkaMessagesAsync<TKey, TValue, TPayload, TSession>(
    IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession> services,
    CancellationToken cancellationToken)
  where TSession : IDisposable
  {
    using var activity = CreateComponentActivity(services.GetActivitySource(), "consume-kafka-messages", ActivityKind.Internal);
    using var logScope = CreateLogScopeForActivity(services.GetLogger(), activity, "consume-kafka-messages");
    var stateActions = GetConsumingStateActions<TKey, TValue, TPayload, TSession>();
    var terminalStates = GetConsumingTerminalStates();
    var metricCounters = services.GetMetricCounters();

    while (!cancellationToken.IsCancellationRequested)
    {
      var currentState = NotStartedState;
      try
      {
        var data = CreateConsumingStepData<TKey, TValue, TPayload>();
        var ctx = CreateConsumingStepContext(services, data, activity);
        await foreach (var state in RunStateMachineAsync(ctx, stateActions, terminalStates, currentState, cancellationToken))
          currentState = state;

        LogConsumedKafkaMessage(services.GetLogger(), currentState);
      }
      catch (OperationCanceledException) { return ConsumingError.None; }
      catch (Exception exception)
      {
        LogConsumeKafkaMessageFailed(services.GetLogger(), exception, currentState);
        IncrementMetricCounter(metricCounters, MetricCounterTypes.ConsumingErrors);

        var error = ToConsumingError(currentState);
        if (error != ConsumingError.None)
          return error;
      }
    }
    return ConsumingError.None;
  }
}