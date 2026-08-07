
namespace Kafka.StateMachines;

internal interface IResumingStepData<TKey, TValue, TPayload>:
  IHandleInboxMessageData<TKey, TValue, TPayload>,
  IScheduleInboxMessageData<TKey, TPayload>,
  IDispatchDeadLetterData<TKey, TValue, TPayload>,
  IDelayDeadLetterData<TKey, TValue, TPayload>;

internal sealed class ResumingStepData<TKey, TValue, TPayload>:
  IResumingStepData<TKey, TValue, TPayload>
{
  public Message<TKey, TValue>? KafkaMessage { get; set; }
  public InboxMessage<TKey, TPayload>? InboxMessage { get; set; }
  public TopicPartitionOffset? TopicPartitionOffset { get; set; }
  public Message<TKey, TValue>? DeadLetter { get; set; }
  public string? HandleError { get; set; }
}