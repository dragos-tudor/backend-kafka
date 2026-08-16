
namespace Kafka.Operations.Outbox;

public interface IInsertingData<TKey, TPayload>:
  IModelProp,
  IOutboxMessageProp<TKey, TPayload>;