using static Kafka.Operations.Outbox.DelayingStates;

namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  internal static async ValueTask<(TData, string)> DelayDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct)
  where TServices : IDelayDeadLetterServices<TKey, TValue, TPayload>
  where TData : IDelayDeadLetterData<TKey, TValue, TPayload>
  {
    var message = data.OutboxMessage!;
    try {
      var currentRetryCount = message.DispatchRetryCount ?? 0;
      var retryOptions = services.GetDelayRetryOptions();

      var nextRetryCount = currentRetryCount + 1;
      var nextAttemptAt = CalculateNextAttemptAt(nextRetryCount, services.GetUtcDate(), retryOptions);
      var status = GetOutboxDeadLetterRetryStatus(currentRetryCount, retryOptions.MaxRetryAttempts);

      await services.UpdateIntegrationMessageAsync(message, message =>
        message
          .SetOutboxMessageLastError(data.DispatchError!)
          .SetOutboxMessageNextAttemptAt(nextAttemptAt)
          .SetOutboxMessageDispatchRetryCount(nextRetryCount)
          .SetOutboxMessageStatus(status), ct);

      if (status == OutboxMessageStatus.Dispatching) {
        InstrumentDelayDeadLetterRetry(message.MessageId, currentRetryCount, data.DispatchError!, services);
        return (data, DelayDeadLetterRetryState);
      }

      InstrumentDelayDeadLetterExhausted(message.MessageId, currentRetryCount, data.DispatchError!, services);
      return (data, DelayDeadLetterExhaustedState);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentDelayDeadLetterError(message.MessageId, ex, services);
      return (data, DelayDeadLetterErrorState);
    }
  }


}
