
namespace Kafka;

internal enum HandleInboxMessageError
{
    HandleInboxMessageFailed,
    MessageDeadLettered,
    OperationCanceled,
}

partial class KafkaFuncs
{
  static ConsumeKafkaMessageError ToConsumeMessageError(HandleInboxMessageError error) =>
    error switch
    {
      HandleInboxMessageError.HandleInboxMessageFailed => ConsumeKafkaMessageError.HandleInboxMessageFailed,
      HandleInboxMessageError.MessageDeadLettered => ConsumeKafkaMessageError.MessageDeadLettered,
      HandleInboxMessageError.OperationCanceled => ConsumeKafkaMessageError.OperationCanceled,
      _ => throw new ArgumentOutOfRangeException(nameof(error), error, null)
    };
}