
namespace Kafka.Operations.Outbox;

public interface IPublishOutboxMessageData<TKey, TValue, TPayload>:
  IOutboxMessageProp<TKey, TPayload>,
  IPublishErrorProp;