using static Kafka.Operations.DeadLetter.InsertingStates;

namespace Kafka.Operations.DeadLetter;

partial class DeadLetterFuncs
{
  internal static async ValueTask<(TData, string)> InsertDeadLetterMessageAsync<TServices, TData, TKey, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices : IInsertingServices<TKey, TPayload>
  where TData : IInsertingData<TKey, TPayload>
  {
    try {
      var message = RequireDeadLetterMessage(data.DeadLetterMessage);
      var messageInserted = await services.InsertDeadLetterMessageAsync(message, ct);
      if (messageInserted is false)
      {
        data.DeadLetterMessage = null;
        InstrumentIdempotentDeadLetterMessage(message.MessageId, services);
        return (data, IdempotentDeadLetterMessageState);
      }

      InstrumentInsertedDeadLetterMessage(message.MessageId, services);
      return (data, InsertedDeadLetterMessageState);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentInsertDeadLetterMessageError(data.DeadLetterMessage?.MessageId, ex, services);
      return (data, InsertDeadLetterMessageErrorState);
    }
  }
}