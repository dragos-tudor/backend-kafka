
namespace Kafka.StateMachines;

public interface IConsumingStepData<TKey, TValue, TPayload>:
  ICaptureKafkaMessageData<TKey, TValue, TPayload>,
  IHandleInboxMessageData<TKey, TValue, TPayload>,
  IInsertInboxMessageData<TKey, TValue, TPayload>,
  IOffsetConsumerData<TKey, TPayload>,
  IDispatchDeadLetterData<TKey, TValue, TPayload>;

public sealed class ConsumingStepData<TKey, TValue, TPayload>:
  IConsumingStepData<TKey, TValue, TPayload>
{
  public Message<TKey, TValue>? KafkaMessage { get; set; }
  public InboxMessage<TKey, TPayload>? InboxMessage { get; set; }
  public TopicPartitionOffset? TopicPartitionOffset { get; set; }
  public Message<TKey, TValue>? DeadLetter { get; set; }
  public string? HandleError { get; set; }
}