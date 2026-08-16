
namespace Kafka.Pipelines;

public interface IConsumingData<TKey, TValue, TPayload>:
  ICapturingData<TKey, TValue>,
  IRedirectingData<TKey, TValue, TPayload>,
  Operations.Inbox.IMappingData<TKey, TValue, TPayload>,
  Operations.Inbox.IValidatingData<TKey, TPayload>,
  Operations.Inbox.IInsertingData<TKey, TPayload>,
  IOffsettingData<TKey, TPayload>,
  IHandlingData<TKey, TValue, TPayload>,
  IConvertingData<TKey, TPayload>,
  Operations.DeadLetter.IInsertingData<TKey, TPayload>,
  Operations.DeadLetter.IMappingData<TKey, TValue, TPayload>,
  IProducingData<TKey, TValue>,
  Operations.DeadLetter.ISchedulingData<TKey, TPayload>;

public sealed class ConsumingData<TKey, TValue, TPayload>:
  IConsumingData<TKey, TValue, TPayload>
{
  public Message<TKey, TValue>? KafkaMessage { get; set; }
  public InboxMessage<TKey, TPayload>? InboxMessage { get; set; }
  public string? InboxMessageError { get; set; }
  public TopicPartitionOffset? TopicPartitionOffset { get; set; }
  public bool TopicPartitionOffsetApplied { get; set; }
  public Message<TKey, TValue?>? KafkaDeadLetter { get; set; }
  public DeadLetterMessage<TKey, TPayload>? DeadLetterMessage { get; set; }
  public string? ProduceError { get; set; }
}

partial class PipelinesFuncs
{
  internal static IConsumingData<TKey, TValue, TPayload> CreateConsumingData<TKey, TValue, TPayload>() =>
    new ConsumingData<TKey, TValue, TPayload>();
}
