
namespace Kafka.Inbox;

partial class InboxFuncs
{
  internal static ValueTask<(TData, string)> CaptureKafkaMessage<TServices, TData, TKey, TValue, TPayload>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices: ICaptureKafkaMessageServices<TKey, TValue>
  where TData : ICaptureKafkaMessageData<TKey, TValue, TPayload>
  {
    try {
      var result = ConsumeMessage(services.GetConsumer(), ct);
      if (!IsValidConsumerMessage(result)) {
        InstrumentNotCapturedKafkaMessage(services);
        return new((data, NotCapturedKafkaMessageState));
      }

      data.KafkaMessage = result.Message;
      data.TopicPartitionOffset = result.TopicPartitionOffset;

      var messageId = GetMessageIdKafkaHeader(result.Message.Headers);
      var correlationId = GetCorrelationIdKafkaHeader(result.Message.Headers);

      InstrumentCapturedKafkaMessage(messageId, correlationId, result.TopicPartitionOffset, services);
      return new((data, CapturedKafkaMessageState));
    }
    catch (Exception ex) {
      InstrumentCaptureKafkaMessageError(ex, services);
      return new((data, CaptureKafkaMessageErrorState));
    }
  }
}