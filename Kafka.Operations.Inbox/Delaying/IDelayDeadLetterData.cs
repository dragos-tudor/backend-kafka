
namespace Kafka.Operations.Inbox;

public interface IDelayDeadLetterData<TKey, TValue, TPayload>:
  IDispatchErrorProp,
  IInboxMessageProp<TKey, TPayload>;
