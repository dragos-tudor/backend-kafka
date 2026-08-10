
namespace Kafka.StateMachines;

internal interface IRelayingData<TKey, TValue, TPayload>:
  IPublishingData<TKey, TValue, TPayload>,
  Operations.Outbox.ISchedulingData<TKey, TPayload>,
  Operations.Outbox.IDispatchingData<TKey, TValue, TPayload>,
  Operations.Outbox.IDelayingData<TKey, TValue, TPayload>;

internal sealed class RelayingData<TKey, TValue, TPayload>:
  IRelayingData<TKey, TValue, TPayload>
{
  public Message<TKey, TValue>? KafkaMessage { get; set; }
  public OutboxMessage<TKey, TPayload>? OutboxMessage { get; set; }
  public Message<TKey, TValue>? DeadLetter { get; set; }
  public string? PublishError { get; set; }
  public string? DispatchError { get; set; }
}

partial class StateMachinesFuncs
{
  internal static IRelayingData<TKey, TValue, TPayload> CreateRelayingData<TKey, TValue, TPayload>(OutboxMessage<TKey, TPayload> message) =>
    new RelayingData<TKey, TValue, TPayload>
    {
      OutboxMessage = message
    };
}