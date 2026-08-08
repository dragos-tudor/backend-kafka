
namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal static async ValueTask<(TData, string)> DelayDeadLetterAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct)
  where TServices : IDelayDeadLetterServices<TKey, TValue, TPayload>
  where TData : IDelayDeadLetterData<TKey, TValue, TPayload>
  {
    var message = data.InboxMessage!;
    try {
      var currentRetryCount = message.PublishRetryCount ?? 0;
      var retryOptions = services.GetDelayRetryOptions();
      var state = GetDelayDeadLetterState(currentRetryCount, retryOptions.MaxRetryAttempts);
      var status = GetDelayDeadLetterStatus(state);

      var nextRetryCount = currentRetryCount + 1;
      var nextAttemptAt = CalculateNextAttemptAt(nextRetryCount, services.GetUtcDate(), retryOptions);

      await services.UpdateIntegrationMessageAsync(message, message =>
        message
          .SetInboxMessageLastError(data.DispatchError!)
          .SetInboxMessageNextAttemptAt(nextAttemptAt)
          .SetInboxMessagePublishRetryCount(nextRetryCount)
          .SetInboxMessageStatus(status), ct);

      if (state == DelayDeadLetterRetryState)
        InstrumentDelayDeadLetterRetry(message.MessageId, currentRetryCount, data.DispatchError!, services);
      if (state == DelayDeadLetterExhaustedState)
        InstrumentDelayDeadLetterExhausted(message.MessageId, currentRetryCount, data.DispatchError!, services);
      return (data, state);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentDelayDeadLetterError(message.MessageId, ex, services);
      return (data, DelayDeadLetterErrorState);
    }
  }


}
