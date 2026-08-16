
namespace Kafka.Operations.DeadLetter;

public interface IMappingData<TKey, TValue, TPayload>:
  IDeadLetterMessageProp<TKey, TPayload>,
  IKafkaDeadLetterProp<TKey, TValue>;