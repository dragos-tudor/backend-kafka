
namespace Kafka.Operations.Inbox;

public interface IDelayingData<TKey, TValue, TPayload>:
  IDispatchErrorProp,
  IInboxMessageProp<TKey, TPayload>;
