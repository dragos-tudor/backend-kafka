
namespace Kafka;

public enum GetConsumerKafkaMessageError
{
  ConsumeKafkaMessageFailed,
  InvalidConsumerMessage,
  OperationCanceled,
}

partial class KafkaFuncs
{
  static ConsumeKafkaMessageError ToConsumeMessageError(GetConsumerKafkaMessageError error) =>
    error switch
    {
      GetConsumerKafkaMessageError.ConsumeKafkaMessageFailed => ConsumeKafkaMessageError.ConsumeKafkaMessageFailed,
      GetConsumerKafkaMessageError.InvalidConsumerMessage => ConsumeKafkaMessageError.InvalidConsumerMessage,
      GetConsumerKafkaMessageError.OperationCanceled => ConsumeKafkaMessageError.OperationCanceled,
      _ => throw new ArgumentOutOfRangeException(nameof(error), error, null),
    };
}