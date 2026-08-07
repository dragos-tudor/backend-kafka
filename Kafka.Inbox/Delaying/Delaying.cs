
namespace Kafka.Inbox;

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
      var messageError = CreateInboxMessageError(
        nextRetryCount,
        data.HandleError!,
        nextAttemptAt,
        status);

      await services.UpdateIntegrationMessageAsync(message, messageError, ct);

      if (state == DelayDeadLetterRetryState)
        InstrumentDelayDeadLetterRetry(message.MessageId, currentRetryCount, data.HandleError!, services);
      if (state == DelayDeadLetterExhaustedState)
        InstrumentDelayDeadLetterExhausted(message.MessageId, currentRetryCount, data.HandleError!, services);
      return (data, state);
    }
    catch (Exception ex) {
      InstrumentDelayDeadLetterError(message.MessageId, ex, services);
      return (data, DelayDeadLetterErrorState);
    }
  }


}
