
namespace Kafka.Operations.Inbox;

internal interface ISchedulingData<TKey, TPayload>:
  IHandleErrorProp,
  IInboxMessageProp<TKey, TPayload>;