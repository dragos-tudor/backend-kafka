
namespace Kafka.Operations.Inbox;

public interface ISchedulingData<TKey, TPayload>:
  IInboxMessageErrorProp,
  IInboxMessageProp<TKey, TPayload>;