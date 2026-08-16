
namespace Kafka.Pipelines;

internal interface IRelayingData<TKey, TValue, TPayload>:
  Operations.Outbox.IMappingData<TKey, TValue, TPayload>,
  IProducingData<TKey, TValue, TPayload>,
  Operations.Outbox.ISchedulingData<TKey, TPayload>;

internal sealed class RelayingData<TKey, TValue, TPayload>:
  IRelayingData<TKey, TValue, TPayload>
{
  public Message<TKey, TValue>? KafkaMessage { get; set; }
  public required OutboxMessage<TKey, TPayload> OutboxMessage { get; set; }
  public Message<TKey, TValue>? DeadLetter { get; set; }
  public string? ProduceError { get; set; }
}

partial class PipelinesFuncs
{
  internal static IRelayingData<TKey, TValue, TPayload> CreateRelayingData<TKey, TValue, TPayload>(
    OutboxMessage<TKey, TPayload> message) =>
    new RelayingData<TKey, TValue, TPayload>
    {
      OutboxMessage = message
    };
}