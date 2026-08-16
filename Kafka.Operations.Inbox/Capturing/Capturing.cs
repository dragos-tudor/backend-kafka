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
      var result = ConsumeMessage(services.GetConsumer(), ct);
      if (IsValidConsumerMessage(result) is false) {
        InstrumentNotCapturedKafkaMessage(services);
        return new((data, NotCapturedKafkaMessageState));
      }

      data.KafkaMessage = result.Message;
      data.TopicPartitionOffset = result.TopicPartitionOffset;

      var messageId = GetMessageIdKafkaHeader(result.Message.Headers);
      var correlationId = GetCorrelationIdKafkaHeader(result.Message.Headers);

      var traceParent = GetTraceParentKafkaHeader(result.Message.Headers);
      InstrumentCapturedKafkaMessage(GetKafkaMessageKey(result.Message), result.TopicPartitionOffset, traceParent, services);
      return new((data, CapturedKafkaMessageState));
    }
    catch (OperationCanceledException) { return default; }
    catch (KafkaException ex) when (ex.Error.IsFatal) {
      InstrumentCaptureKafkaMessageCriticalError(ex, services);
      return new((data, CaptureKafkaMessageCriticalErrorState));
    }
    catch (Exception ex) {
      InstrumentCaptureKafkaMessageError(ex, services);
      return new((data, CaptureKafkaMessageErrorState));
    }
  }
}