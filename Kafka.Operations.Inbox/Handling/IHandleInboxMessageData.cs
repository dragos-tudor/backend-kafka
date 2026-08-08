
namespace Kafka.Operations.Inbox;

public interface IHandleInboxMessageData<TKey, TValue, TPayload>:
  IInboxMessageProp<TKey, TPayload>,
  IHandleErrorProp;