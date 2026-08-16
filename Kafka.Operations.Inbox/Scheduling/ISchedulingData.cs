
namespace Kafka.Operations.Inbox;

internal interface ISchedulingData<TKey, TPayload>:
  IInboxMessageErrorProp,
  IInboxMessageProp<TKey, TPayload>;