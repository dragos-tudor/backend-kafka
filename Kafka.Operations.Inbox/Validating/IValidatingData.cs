
namespace Kafka.Operations.Inbox;

public interface IValidatingData<TKey, TPayload>:
  IInboxMessageProp<TKey, TPayload>,
  IInboxMessageErrorProp;