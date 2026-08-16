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
    try {
      var message = RequireInboxMessage(data.InboxMessage);
      var error = RequireInboxMessageError(data.InboxMessageError);
      var currentRetryCount = message.RetryCount ?? 0;
      var retryOptions = services.GetRetryMessageOptions();

      var nextRetryCount = currentRetryCount + 1;
      var nextAttemptAt = CalculateNextAttemptAt(nextRetryCount, services.GetUtcDate(), retryOptions);
      var status = GetInboxMessageStatus(currentRetryCount, retryOptions.MaxRetryAttempts);

      await services.UpdateInboxMessageAsync(message, message =>
        SetInboxMessageStatus(message, status)
          .SetInboxMessageLastError(error)
          .SetInboxMessageNextAttemptAt(nextAttemptAt)
          .SetInboxMessageRetryCount(nextRetryCount), ct);

      if (status == InboxMessageStatus.Processing) {
        InstrumentScheduleInboxMessageRetry(message.MessageId, currentRetryCount, error, services);
        return (data, ScheduleInboxMessageRetryState);
      }

      InstrumentScheduleInboxMessageExhausted(message.MessageId, currentRetryCount, error, services);
      return (data, ScheduleInboxMessageExhaustedState);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentScheduleInboxMessageError(data.InboxMessage?.MessageId, ex, services);
      return (data, ScheduleInboxMessageErrorState);
    }
  }
}
