
namespace Kafka.Messages;

partial class MessagesFuncs
{
  const int MaxDeadLetterRetries = 5;

  internal static DeadLetterMessageStatus GetDeadLetterMessageStatus(
    int currentRetryCount,
    int maxRetries = MaxDeadLetterRetries) =>
      currentRetryCount + 1 <= maxRetries
          ? DeadLetterMessageStatus.Pending
          : DeadLetterMessageStatus.Abandoned;
}