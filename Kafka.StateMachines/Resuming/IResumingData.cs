
namespace Kafka.StateMachines;

internal interface IResumingData<TKey, TValue, TPayload>:
  IHandlingData<TKey, TValue, TPayload>,
  Operations.Inbox.ISchedulingData<TKey, TPayload>,
  Operations.Inbox.IDispatchingData<TKey, TValue, TPayload>,
  Operations.Inbox.IDelayingData<TKey, TValue, TPayload>;

internal sealed class ResumingData<TKey, TValue, TPayload>:
  IResumingData<TKey, TValue, TPayload>
{
  public Message<TKey, TValue>? KafkaMessage { get; set; }
  public InboxMessage<TKey, TPayload>? InboxMessage { get; set; }
  public TopicPartitionOffset? TopicPartitionOffset { get; set; }
  public Message<TKey, TValue>? DeadLetter { get; set; }
  public string? HandleError { get; set; }
  public string? DispatchError { get; set; }
}

partial class StateMachinesFuncs
{
  internal static IResumingData<TKey, TValue, TPayload> CreateResumingData<TKey, TValue, TPayload>(InboxMessage<TKey, TPayload> message) =>
    new ResumingData<TKey, TValue, TPayload>
    {
      InboxMessage = message
    };
}