
namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static ConsumeResult<TKey, TValue>? CaptureKafkaMessage<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    CancellationToken cancellationToken)
  {
    var result = ConsumeMessage(consumer, cancellationToken);
    return IsValidConsumerMessage(result) ? result : default;
  }
}