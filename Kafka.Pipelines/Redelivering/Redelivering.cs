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
    IReadOnlyList<DeadLetterMessage<TKey, TPayload>> messages;
    try
    {
      var batchSize = services.GetRelayBatchSize();
      var utcDate = services.GetUtcDate();
      messages = await services.GetDeadLetterMessagesAsync(utcDate, batchSize, ct);
    }
    catch(OperationCanceledException) { return default; }
    catch(Exception exception)
    {
      InstrumentFetchOutboxMessageError(exception, services);
      return RedeliveringCriticalErrorState;
    }

    foreach (var message in messages)
    {
      using var activity = CreateDefaultActivity(services.GetActivitySource(), "redeliver.dead.letter.message", ActivityKind.Internal);
      using var logScope = CreateComponentLogScope(services.GetLogger(), activity, "redeliver.dead.letter.message");

      var currentData = (TData)CreateRedeliveringData<TKey, TValue, TPayload>(message);
      var currentState = RedeliveringNotStartedState;
      var getStateAction = GetRedeliveringStateAction<TServices, TData, TKey, TValue, TPayload>;

      await foreach (var (newData, newState) in RunStateMachineAsync(services, currentData, currentState, getStateAction, ct))
      {
        if (RedeliveringCriticalStates.Contains(newState))
        {
          InstrumentRedeliverDeadLetterMessageCriticalError(newState, services);
          return RedeliveringCriticalErrorState;
        }
        currentData = newData;
        currentState = newState;
      }
      InstrumentRedeliveredDeadLetterMessage(message.MessageId, currentState, services);
    }
    return default;
  }
}