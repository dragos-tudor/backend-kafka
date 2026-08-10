
namespace Kafka.Operations.Outbox;

internal interface ISchedulingData<TKey, TPayload>:
  IPublishErrorProp,
  IOutboxMessageProp<TKey, TPayload>;