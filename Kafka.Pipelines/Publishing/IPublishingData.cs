
namespace Kafka.Pipelines;

public interface IPublishingData<TKey, TValue, TPayload>:
  Operations.Outbox.IValidatingData<TKey, TPayload>,
  Operations.Outbox.IInsertingData<TKey, TPayload>,
  Operations.Outbox.IMappingData<TKey, TValue, TPayload>,
  Operations.Outbox.IProducingData<TKey, TValue, TPayload>,
  Operations.Outbox.ISchedulingData<TKey, TPayload>;

public sealed class PublishingData<TKey, TValue, TPayload>:
  IPublishingData<TKey, TValue, TPayload>
{
  public Message<TKey, TValue>? KafkaMessage { get; set; }
  public required OutboxMessage<TKey, TPayload> OutboxMessage { get; set; }
  public Message<TKey, TValue?>? KafkaDeadLetter { get; set; }
  public string? ProduceError { get; set; }
  public required object Model { get; init; }
}

partial class PipelinesFuncs
{
  internal static IPublishingData<TKey, TValue, TPayload> CreatePublishingData<TKey, TValue, TPayload, TModel>(
    OutboxMessage<TKey, TPayload> message,
    TModel model) =>
    new PublishingData<TKey, TValue, TPayload>
    {
      OutboxMessage = message,
      Model = model!
    };
}