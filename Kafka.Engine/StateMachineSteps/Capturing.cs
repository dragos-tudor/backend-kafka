using static Kafka.Engine.ConsumingState;

namespace Kafka.Engine;

partial class EngineFuncs
{
  internal static ValueTask<ConsumingState> CaptureKafkaMessageStepAsync<TService, TData, TKey, TValue, TPayload>(
    StepContext<TService, TData> ctx,
    CancellationToken ct)
  where TService: ICaptureKafkaMessageServices<TKey, TValue>
  where TData : ICaptureKafkaMessageData<TKey, TValue, TPayload>
  {
    var services = ctx.Services;
    var data = ctx.Data;

    var result = CaptureKafkaMessage(services.GetConsumer(), ct);
    if (result is null) return new(NotCapturedKafkaMessageState);

    data.KafkaMessage = result.Message;
    data.Offset = result.TopicPartitionOffset;
    data.MessageId = GetMessageIdKafkaHeader(result.Message.Headers);
    data.CorrelationId = GetCorrelationIdKafkaHeader(result.Message.Headers);

    InstrumentCaptureKafkaMessageStep(ctx.Activity, services);
    return new(CapturedKafkaMessageState);
  }
}