
namespace Kafka.Operations.Inbox;

public interface IInsertingData<TKey, TPayload>:
  IInboxMessageProp<TKey, TPayload>;