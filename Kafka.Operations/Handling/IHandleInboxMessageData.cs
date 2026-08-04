
namespace Kafka.Operations;

public interface IHandleInboxMessageData<TKey, TValue, TPayload>:
  IInboxMessageProp<TKey, TPayload>,
  IDomainErrorProp;