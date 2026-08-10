using static Kafka.Operations.Outbox.SchedulingStates;

namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  internal static async ValueTask<(TData, string)> ScheduleOutboxMessageAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct)
  where TServices : ISchedulingServices<TKey, TPayload>
  where TData : ISchedulingData<TKey, TPayload>
  {
    var message = data.OutboxMessage!;
    try {
      var currentRetryCount = message.RetryCount ?? 0;
      var retryOptions = services.GetScheduleRetryOptions();

      var nextRetryCount = currentRetryCount + 1;
      var nextAttemptAt = CalculateNextAttemptAt(nextRetryCount, services.GetUtcDate(), retryOptions);
      var status = GetOutboxMessageRetryStatus(currentRetryCount, retryOptions.MaxRetryAttempts);

      await services.UpdateIntegrationMessageAsync(message, message =>
        message
          .SetOutboxMessageLastError(data.PublishError!)
          .SetOutboxMessageNextAttemptAt(nextAttemptAt)
          .SetOutboxMessageRetryCount(nextRetryCount)
          .SetOutboxMessageStatus(status), ct);

      if (status == OutboxMessageStatus.Pending) {
        InstrumentScheduleOutboxMessageRetry(message.MessageId, currentRetryCount, data.PublishError!, services);
        return (data, ScheduleOutboxMessageRetryState);
      }

      InstrumentScheduleOutboxMessageExhausted(message.MessageId, currentRetryCount, data.PublishError!, services);
      return (data, ScheduleOutboxMessageExhaustedState);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentScheduleOutboxMessageError(message.MessageId, ex, services);
      return (data, ScheduleOutboxMessageErrorState);
    }
  }
}
