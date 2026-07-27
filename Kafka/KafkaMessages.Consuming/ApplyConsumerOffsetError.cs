
namespace Kafka;

public enum ApplyConsumerOffsetError
{
  ApplyConsumerOffsetFailed,
}

partial class KafkaFuncs
{
  static ConsumeKafkaMessageError ToConsumeMessageError(ApplyConsumerOffsetError error) =>
    error switch
    {
      ApplyConsumerOffsetError.ApplyConsumerOffsetFailed => ConsumeKafkaMessageError.ApplyOffsetFailed,
      _ => throw new ArgumentOutOfRangeException(nameof(error), error, null),
    };
}