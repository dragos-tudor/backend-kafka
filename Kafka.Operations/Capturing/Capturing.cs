using static Kafka.Operations.OperationState;

namespace Kafka.Operations;

partial class OperationsFuncs
{
  internal static ConsumeResult<TKey, TValue>? CaptureKafkaMessage<TKey, TValue>(
    IConsumer<TKey, TValue> consumer,
    CancellationToken cancellationToken)
  {
    var result = ConsumeMessage(consumer, cancellationToken);
    return IsValidConsumerMessage(result) ? result : default;
  }

  internal static ValueTask<(TData, OperationState)> CaptureKafkaMessage<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct)
  where TServices: ICaptureKafkaMessageServices<TKey, TValue>
  where TData : ICaptureKafkaMessageData<TKey, TValue, TPayload>
  {
    var result = CaptureKafkaMessage(services.GetConsumer(), ct);
    if (result is null) return new((data, NotCapturedKafkaMessageState));

    data.KafkaMessage = result.Message;
    data.TopicPartitionOffset = result.TopicPartitionOffset;

    var messageId = GetMessageIdKafkaHeader(result.Message.Headers);
    var correlationId = GetCorrelationIdKafkaHeader(result.Message.Headers);

    InstrumentCaptureKafkaMessage(messageId, correlationId, result.TopicPartitionOffset, services);
    return new((data, CapturedKafkaMessageState));
  }
}