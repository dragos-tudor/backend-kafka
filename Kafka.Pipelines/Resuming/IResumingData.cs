
namespace Kafka.Pipelines;

internal interface IResumingData<TKey, TValue, TPayload>:
  IHandlingData<TKey, TValue, TPayload>,
  IConvertingData<TKey, TPayload>,
  Operations.DeadLetter.IInsertingData<TKey, TPayload>,
  Operations.DeadLetter.IMappingData<TKey, TValue, TPayload>,
  IProducingData<TKey, TValue>,
  Operations.DeadLetter.ISchedulingData<TKey, TPayload>;

internal sealed class ResumingData<TKey, TValue, TPayload>:
  IResumingData<TKey, TValue, TPayload>
{
  public Message<TKey, TValue>? KafkaMessage { get; set; }
  public InboxMessage<TKey, TPayload>? InboxMessage { get; set; }
  public string? InboxMessageError { get; set; }
  public TopicPartitionOffset? TopicPartitionOffset { get; set; }
  public bool TopicPartitionOffsetApplied { get; set; }
  public DeadLetterMessage<TKey, TPayload>? DeadLetterMessage { get; set; }
  public Message<TKey, TValue?>? KafkaDeadLetter { get; set; }
  public string? ProduceError { get; set; }
}

partial class PipelinesFuncs
{
  internal static IResumingData<TKey, TValue, TPayload> CreateResumingData<TKey, TValue, TPayload>(
    InboxMessage<TKey, TPayload> message) =>
    new ResumingData<TKey, TValue, TPayload>
    {
      InboxMessage = message,
    };
}