using static Kafka.Operations.Inbox.SchedulingStates;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal static async ValueTask<(TData, string)> ScheduleInboxMessageAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct)
  where TServices : ISchedulingServices<TKey, TPayload>
  where TData : ISchedulingData<TKey, TPayload>
  {
    var message = data.InboxMessage!;
    try {
      var currentRetryCount = message.RetryCount ?? 0;
      var retryOptions = services.GetScheduleRetryOptions();

      var nextRetryCount = currentRetryCount + 1;
      var nextAttemptAt = CalculateNextAttemptAt(nextRetryCount, services.GetUtcDate(), retryOptions);
      var status = GetInboxMessageRetryStatus(currentRetryCount, retryOptions.MaxRetryAttempts);

      await services.UpdateIntegrationMessageAsync(message, message =>
        message
          .SetInboxMessageLastError(data.HandleError!)
          .SetInboxMessageNextAttemptAt(nextAttemptAt)
          .SetInboxMessageRetryCount(nextRetryCount)
          .SetInboxMessageStatus(status), ct);

      if (status == InboxMessageStatus.Pending) {
        InstrumentScheduleInboxMessageRetry(message.MessageId, currentRetryCount, data.HandleError!, services);
        return (data, ScheduleInboxMessageRetryState);
      }

      InstrumentScheduleInboxMessageExhausted(message.MessageId, currentRetryCount, data.HandleError!, services);
      return (data, ScheduleInboxMessageExhaustedState);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentScheduleInboxMessageError(message.MessageId, ex, services);
      return (data, ScheduleInboxMessageErrorState);
    }
  }
}
