
namespace Kafka.Operations.Outbox;

public interface IDelayDeadLetterData<TKey, TValue, TPayload>:
  IDispatchErrorProp,
  IOutboxMessageProp<TKey, TPayload>;
