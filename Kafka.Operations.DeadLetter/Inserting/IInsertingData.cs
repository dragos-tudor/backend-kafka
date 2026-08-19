
namespace Kafka.Operations.DeadLetter;

public interface IInsertingData<TKey, TPayload>:
  IDeadLetterMessageProp<TKey, TPayload>,
  IInboxMessageProp<TKey, TPayload>;