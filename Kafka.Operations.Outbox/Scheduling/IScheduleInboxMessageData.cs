
namespace Kafka.Operations.Outbox;

internal interface IScheduleOutboxMessageData<TKey, TPayload>:
  IPublishErrorProp,
  IOutboxMessageProp<TKey, TPayload>;