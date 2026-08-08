
namespace Kafka.Operations.Outbox;

public interface IDispatchDeadLetterData<TKey, TValue, TPayload>:
  IOutboxMessageProp<TKey, TPayload>,
  IDeadLetterProp<TKey, TValue>,
  IDispatchErrorProp,
  IPublishErrorProp;