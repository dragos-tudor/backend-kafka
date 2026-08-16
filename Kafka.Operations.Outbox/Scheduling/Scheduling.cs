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
    try {
      var message = data.OutboxMessage;
      var produceError = RequireProduceError(data.ProduceError);
      var currentRetryCount = message.RetryCount ?? 0;
      var retryOptions = services.GetRetryMessageOptions();

      var nextRetryCount = currentRetryCount + 1;
      var nextAttemptAt = CalculateNextAttemptAt(nextRetryCount, services.GetUtcDate(), retryOptions);
      var status = GetOutboxMessageStatus(currentRetryCount, retryOptions.MaxRetryAttempts);

      await services.UpdateOutboxMessageAsync(message, message =>
          SetOutboxMessageStatus(message, status)
          .SetOutboxMessageLastError(produceError)
          .SetOutboxMessageNextAttemptAt(nextAttemptAt)
          .SetOutboxMessageRetryCount(nextRetryCount),
          ct);

      if (status == OutboxMessageStatus.Pending) {
        InstrumentScheduleOutboxMessageRetry(message.MessageId, currentRetryCount, produceError, services);
        return (data, ScheduleOutboxMessageRetryState);
      }

      InstrumentScheduleOutboxMessageExhausted(message.MessageId, currentRetryCount, produceError, services);
      return (data, ScheduleOutboxMessageExhaustedState);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentScheduleOutboxMessageError(data.OutboxMessage.MessageId, ex, services);
      return (data, ScheduleOutboxMessageErrorState);
    }
  }
}
