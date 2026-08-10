
namespace Kafka.Operations.Outbox;

public interface IDelayingData<TKey, TValue, TPayload>:
  IDispatchErrorProp,
  IOutboxMessageProp<TKey, TPayload>;
