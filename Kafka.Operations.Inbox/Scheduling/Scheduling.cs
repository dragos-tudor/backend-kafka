
namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal static async ValueTask<(TData, string)> ScheduleInboxMessageAsync<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct)
  where TServices : IScheduleInboxMessageServices<TKey, TPayload>
  where TData : IScheduleInboxMessageData<TKey, TPayload>
  {
    var message = data.InboxMessage!;
    try {
      var currentRetryCount = message.RetryCount ?? 0;
      var retryOptions = services.GetScheduleRetryOptions();
      var state = GetScheduleInboxMessageState(currentRetryCount, retryOptions.MaxRetryAttempts);
      var status = GetScheduleInboxMessageStatus(state);

      var nextRetryCount = currentRetryCount + 1;
      var nextAttemptAt = CalculateNextAttemptAt(nextRetryCount, services.GetUtcDate(), retryOptions);
      var messageError = CreateInboxMessageError(
        nextRetryCount,
        data.HandleError!,
        nextAttemptAt,
        status);

      await services.UpdateIntegrationMessageAsync(message, messageError, ct);

      if (state == ScheduleInboxMessageRetryState)
        InstrumentScheduleInboxMessageRetry(message.MessageId, currentRetryCount, data.HandleError!, services);
      if (state == ScheduleInboxMessageExhaustedState)
        InstrumentScheduleInboxMessageExhausted(message.MessageId, currentRetryCount, data.HandleError!, services);
      return (data, state);
    }
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentScheduleInboxMessageError(message.MessageId, ex, services);
      return (data, ScheduleInboxMessageErrorState);
    }
  }
}
