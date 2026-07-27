
namespace Kafka;

internal enum SaveInboxMessageError
{
    SaveInboxMessageFailed,
    InboxMessageAlreadySaved,
    OperationCanceled
}

partial class KafkaFuncs
{
  static ConsumeKafkaMessageError ToConsumeMessageError(SaveInboxMessageError error) =>
    error switch
    {
      SaveInboxMessageError.SaveInboxMessageFailed => ConsumeKafkaMessageError.SaveInboxMessageFailed,
      SaveInboxMessageError.InboxMessageAlreadySaved => ConsumeKafkaMessageError.InboxMessageAlreadySaved,
      SaveInboxMessageError.OperationCanceled => ConsumeKafkaMessageError.OperationCanceled,
      _ => throw new ArgumentOutOfRangeException(nameof(error), error, null)
    };
}