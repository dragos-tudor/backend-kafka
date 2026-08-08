
namespace Kafka.Operations.Inbox;

internal interface IScheduleInboxMessageData<TKey, TPayload>:
  IHandleErrorProp,
  IInboxMessageProp<TKey, TPayload>;