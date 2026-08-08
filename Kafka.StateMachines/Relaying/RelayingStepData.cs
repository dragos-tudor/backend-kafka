
namespace Kafka.StateMachines;

internal interface IRelayingStepData<TKey, TValue, TPayload>:
  IPublishOutboxMessageData<TKey, TValue, TPayload>,
  IScheduleOutboxMessageData<TKey, TPayload>,
  Operations.Outbox.IDispatchDeadLetterData<TKey, TValue, TPayload>,
  Operations.Outbox.IDelayDeadLetterData<TKey, TValue, TPayload>;

internal sealed class RelayingStepData<TKey, TValue, TPayload>:
  IRelayingStepData<TKey, TValue, TPayload>
{
  public Message<TKey, TValue>? KafkaMessage { get; set; }
  public OutboxMessage<TKey, TPayload>? OutboxMessage { get; set; }
  public Message<TKey, TValue>? DeadLetter { get; set; }
  public string? PublishError { get; set; }
  public string? DispatchError { get; set; }
}

partial class StateMachinesFuncs
{
  internal static IRelayingStepData<TKey, TValue, TPayload> CreateRelayingStepData<TKey, TValue, TPayload>(OutboxMessage<TKey, TPayload> message) =>
    new RelayingStepData<TKey, TValue, TPayload>
    {
      OutboxMessage = message
    };
}