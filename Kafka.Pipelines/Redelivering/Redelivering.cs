using static Kafka.Pipelines.RedeliveringStates;

namespace Kafka.Pipelines;

partial class PipelinesFuncs
{
  internal static async Task<string?> RedeliverDeadLetterMessagesAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    CancellationToken ct = default)
  where TServices : IRedeliveringServices<TKey, TValue, TPayload>
  where TData : IRedeliveringData<TKey, TValue, TPayload>
  {
    var messages = await GetRedeliveringDeadLetterMessagesAsync<TServices, TData, TKey, TValue, TPayload>(services, ct);
    if (messages is null) return default;

    foreach (var message in messages)
    {
      using var activity = CreateDefaultActivity(services.GetActivitySource(), "redeliver.dead.letter.message", ActivityKind.Internal);
      using var logScope = CreateComponentLogScope(services.GetLogger(), activity, "redeliver.dead.letter.message");

      var initialData = (TData)CreateRedeliveringData<TKey, TValue, TPayload>(message);
      var initialState = RedeliveringNotStartedState;
      var getStateAction = GetRedeliveringStateAction<TServices, TData, TKey, TValue, TPayload>;

      var (_, lastState) = await RunStateMachineAsync(services, initialData, initialState, getStateAction, ct);
      if (RedeliveringCriticalStates.Contains(lastState))
      {
        InstrumentRedeliverDeadLetterMessageCriticalError(lastState, services);
        return RedeliveringCriticalErrorState;
      }
      InstrumentRedeliveredDeadLetterMessage(message.MessageId, lastState, services);
    }
    return default;
  }
}