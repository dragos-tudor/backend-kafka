using static Kafka.Messages.MessageFieldConstraints;

namespace Kafka.Messages;

partial class MessagesFuncs
{
  const string TruncationSuffix = " …[truncated]";

  internal static string TruncateDeadLetterMessageFailureReason(string failureReason) =>
    failureReason.Length <= FailureReasonMaxLength?
      failureReason:
      string.Concat(failureReason.AsSpan(0, FailureReasonMaxLength - TruncationSuffix.Length), TruncationSuffix);
}