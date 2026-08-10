
namespace Kafka.Operations.Outbox;

public interface IDispatchingData<TKey, TValue, TPayload>:
  IOutboxMessageProp<TKey, TPayload>,
  IDeadLetterProp<TKey, TValue>,
  IDispatchErrorProp,
  IPublishErrorProp;