using static Kafka.Operations.DeadLetter.SchedulingStates;

namespace Kafka.Operations.DeadLetter;

partial class DeadLetterFuncs
{
  internal static async ValueTask<(TData, string)> ScheduleDeadLetterMessageAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct)
  where TServices : ISchedulingServices<TKey, TPayload>
  where TData : ISchedulingData<TKey, TPayload>
  {
    try {
      var message = RequireDeadLetterMessage(data.DeadLetterMessage);
      var produceError = RequireProduceError(data.ProduceError);
      var currentRetryCount = message.RetryCount ?? 0;
      var retryOptions = services.GetRetryMessageOptions();

      var nextRetryCount = currentRetryCount + 1;
      var nextAttemptAt = CalculateNextAttemptAt(nextRetryCount, services.GetUtcDate(), retryOptions);
      var status = GetDeadLetterMessageStatus(currentRetryCount, retryOptions.MaxRetryAttempts);

      await services.UpdateDeadLetterAsync(message, message =>
          SetDeadLetterMessageStatus(message, status)
          .SetDeadLetterMessageLastError(produceError)
          .SetDeadLetterMessageNextAttemptAt(nextAttemptAt)
          .SetDeadLetterMessageRetryCount(nextRetryCount),
          ct);

      if (status == DeadLetterMessageStatus.Pending) {
        InstrumentScheduleDeadLetterMessageRetry(message.MessageId, currentRetryCount, produceError, services);
        return (data, ScheduleDeadLetterMessageRetryState);
      }

      InstrumentScheduleDeadLetterMessageExhausted(message.MessageId, currentRetryCount, produceError, services);
      return (data, ScheduleDeadLetterMessageExhaustedState);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentScheduleDeadLetterMessageError(data.DeadLetterMessage?.MessageId, ex, services);
      return (data, ScheduleDeadLetterMessageErrorState);
    }
  }
}
