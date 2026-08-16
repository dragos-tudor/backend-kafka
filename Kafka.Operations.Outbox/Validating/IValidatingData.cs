
namespace Kafka.Operations.Outbox;

public interface IValidatingData<TKey, TPayload>:
  IOutboxMessageProp<TKey, TPayload>;