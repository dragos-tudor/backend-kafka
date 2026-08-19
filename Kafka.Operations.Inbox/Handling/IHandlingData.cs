
namespace Kafka.Operations.Inbox;

public interface IHandlingData<TKey, TPayload>:
  IInboxMessageProp<TKey, TPayload>,
  IInboxMessageErrorProp;