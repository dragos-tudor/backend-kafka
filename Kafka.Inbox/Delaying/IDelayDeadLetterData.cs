
namespace Kafka.Inbox;

public interface IDelayDeadLetterData<TKey, TValue, TPayload>:
  IHandleErrorProp,
  IInboxMessageProp<TKey, TPayload>;
