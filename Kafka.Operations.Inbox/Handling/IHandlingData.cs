
namespace Kafka.Operations.Inbox;

public interface IHandlingData<TKey, TValue, TPayload>:
  IInboxMessageProp<TKey, TPayload>,
  IInboxMessageErrorProp;