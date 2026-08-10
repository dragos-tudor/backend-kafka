using static Kafka.Operations.Inbox.CapturingStates;

namespace Kafka.Operations.Inbox;

partial class InboxFuncs
{
  internal static ValueTask<(TData, string)> CaptureKafkaMessage<TServices, TData, TKey, TValue>(
    TServices services,
    TData data,
    CancellationToken ct = default)
  where TServices: ICapturingServices<TKey, TValue>
  where TData : ICapturingData<TKey, TValue>
  {
    try {
      var result = ConsumeMessage(services.GetConsumer(data.Pipeline), ct);
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
    catch (OperationCanceledException) { return default; }
    catch (Exception ex) {
      InstrumentCaptureKafkaMessageError(ex, services);
      return ex is KafkaException
        ? new((data, CaptureKafkaMessageCriticalErrorState))
        : new((data, CaptureKafkaMessageErrorState));
    }
  }
}