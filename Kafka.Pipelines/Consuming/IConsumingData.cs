


namespace Kafka.Pipelines;

public interface IConsumingData<TKey, TValue, TPayload>:
  ICapturingData<TKey, TValue>,
  IHandlingData<TKey, TValue, TPayload>,
  IInsertingData<TKey, TValue, TPayload>,
  IOffsettingData<TKey, TPayload>,
  Operations.Inbox.IDispatchingData<TKey, TValue, TPayload>;

public sealed class ConsumingData<TKey, TValue, TPayload>:
  IConsumingData<TKey, TValue, TPayload>
{
  public Message<TKey, TValue>? KafkaMessage { get; set; }
  public InboxMessage<TKey, TPayload>? InboxMessage { get; set; }
  public TopicPartitionOffset? TopicPartitionOffset { get; set; }
  public bool TopicPartitionOffsetApplied { get; set; }
  public Message<TKey, TValue>? DeadLetter { get; set; }
  public string? HandleError { get; set; }
  public string? DispatchError { get; set; }
  public required string Pipeline { get; init; }
}

partial class PipelinesFuncs
{
  internal static IConsumingData<TKey, TValue, TPayload> CreateConsumingData<TKey, TValue, TPayload>(PipelineType pipelineType) =>
    new ConsumingData<TKey, TValue, TPayload>
    {
      Pipeline = pipelineType.ToString()
    };
}
