
namespace Kafka.Operations.DeadLetter;

public interface IConvertingData<TKey, TPayload>:
  IDeadLetterMessageProp<TKey, TPayload>,
  IInboxMessageProp<TKey, TPayload>,
  IInboxMessageErrorProp;