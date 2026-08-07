
namespace Kafka.Inbox;

partial class InboxFuncs
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

  internal static InboxMessageStatus GetDelayDeadLetterStatus(
    string state) =>
      state switch
      {
        DelayDeadLetterExhaustedState => InboxMessageStatus.Abandoned,
        DelayDeadLetterRetryState => InboxMessageStatus.DeadLettering,
        _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
      };
}