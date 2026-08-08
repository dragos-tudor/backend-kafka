
namespace Kafka.Operations.Outbox;

partial class OutboxFuncs
{
  internal const string DelayDeadLetterExhaustedState = "DelayDeadLetterExhaustedState";
  internal const string DelayDeadLetterRetryState = "DelayDeadLetterRetryState";
  internal const string DelayDeadLetterErrorState = "DelayDeadLetterErrorState";

  const int MaxDelayRetries = 5;

  internal static string GetDelayDeadLetterState(
    int currentRetryCount,
    int maxDelayRetries = MaxDelayRetries) =>
      currentRetryCount + 1 < maxDelayRetries
          ? DelayDeadLetterRetryState
          : DelayDeadLetterExhaustedState;

  internal static OutboxMessageStatus GetDelayDeadLetterStatus(
    string state) =>
      state switch
      {
        DelayDeadLetterExhaustedState => OutboxMessageStatus.Abandoned,
        DelayDeadLetterRetryState => OutboxMessageStatus.DeadLettering,
        _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
      };
}