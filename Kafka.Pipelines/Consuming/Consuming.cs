using static Kafka.Operations.Inbox.RedirectingStates;
using static Kafka.Operations.Inbox.InsertingStates;
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
      using var activity = CreateDefaultActivity(services.GetActivitySource(), "consuming.kafka.message", ActivityKind.Consumer);
      using var logScope = CreateComponentLogScope(services.GetLogger(), activity, "consuming.kafka.message");

      var initialData = (TData)CreateConsumingData<TKey, TValue, TPayload>();
      var initialState = ConsumingNotStartedState;
      var kafkaOptions = services.GetKafkaOptions();

      var (_, lastState) = await RunConsumingStateMachineAsync<TServices, TData, TKey, TValue, TPayload, TSession>
        (services, initialData, initialState, kafkaOptions, default, ct);

      if (ConsumingCriticalStates.Contains(lastState ?? string.Empty)) {
        InstrumentConsumeKafkaMessageCriticalError(lastState, services);
        return lastState;
      }
      InstrumentConsumeKafkaMessage(lastState, services);
    }
    return default;
  }
}