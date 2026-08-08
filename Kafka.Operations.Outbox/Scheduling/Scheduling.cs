
namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  internal static async ValueTask<(TData, string)> ScheduleOutboxMessageAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct)
  where TServices : IScheduleOutboxMessageServices<TKey, TPayload>
  where TData : IScheduleOutboxMessageData<TKey, TPayload>
  {
    var message = data.OutboxMessage!;
    try {
      var currentRetryCount = message.RetryCount ?? 0;
      var retryOptions = services.GetScheduleRetryOptions();
      var state = GetScheduleOutboxMessageState(currentRetryCount, retryOptions.MaxRetryAttempts);
      var status = GetScheduleOutboxMessageStatus(state);

      var nextRetryCount = currentRetryCount + 1;
      var nextAttemptAt = CalculateNextAttemptAt(nextRetryCount, services.GetUtcDate(), retryOptions);

      await services.UpdateIntegrationMessageAsync(message, message =>
        message
          .SetOutboxMessageLastError(data.PublishError!)
          .SetOutboxMessageNextAttemptAt(nextAttemptAt)
          .SetOutboxMessageRetryCount(nextRetryCount)
          .SetOutboxMessageStatus(status), ct);

      if (state == ScheduleOutboxMessageRetryState)
        InstrumentScheduleOutboxMessageRetry(message.MessageId, currentRetryCount, data.PublishError!, services);
      if (state == ScheduleOutboxMessageExhaustedState)
        InstrumentScheduleOutboxMessageExhausted(message.MessageId, currentRetryCount, data.PublishError!, services);
      return (data, state);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentScheduleOutboxMessageError(message.MessageId, ex, services);
      return (data, ScheduleOutboxMessageErrorState);
    }
  }
}
