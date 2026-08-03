
namespace Kafka.Engine;

public interface IConsumingStepData<TKey, TValue, TPayload>:
  IApplyConsumerOffsetData,
  ICaptureKafkaMessageData<TKey, TValue, TPayload>,
  IHandleInboxMessageData<TKey, TValue, TPayload>,
  IInsertInboxMessageData<TKey, TValue, TPayload>,
  IPublishDeadLetterData<TKey, TValue, TPayload>;

public sealed class ConsumingStepData<TKey, TValue, TPayload>:
  IConsumingStepData<TKey, TValue, TPayload>
{
  public Message<TKey, TValue>? KafkaMessage { get; set; }
  public InboxMessage<TKey, TPayload>? Message { get; set; }
  public TopicPartitionOffset? Offset { get; set; }
  public long? AppliedOffset { get; set; }
  public Guid? MessageId { get; set; }
  public Guid? CorrelationId { get; set; }
  public string? DomainError { get; set; }
}

partial class EngineFuncs
{
  static ConsumingStepData<TKey, TValue, TPayload> CreateConsumingStepData<TKey, TValue, TPayload>() =>
    new();
}