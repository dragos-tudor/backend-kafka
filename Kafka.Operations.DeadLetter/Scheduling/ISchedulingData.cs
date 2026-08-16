
namespace Kafka.Operations.DeadLetter;

public interface ISchedulingData<TKey, TPayload>:
  IDeadLetterMessageProp<TKey, TPayload>,
  IProduceErrorProp;