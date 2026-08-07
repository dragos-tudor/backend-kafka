
namespace Kafka.Messages;

partial class MessagesFuncs
{
  internal static InboxMessageError CreateInboxMessageError(
    int retryCount,
    string? error,
    DateTime nextAttempAt,
    InboxMessageStatus status)
  =>
    new()
    {
      RetryCount = retryCount,
      LastError = error,
      NextAttemptAt = nextAttempAt,
      Status = status,
    };
}