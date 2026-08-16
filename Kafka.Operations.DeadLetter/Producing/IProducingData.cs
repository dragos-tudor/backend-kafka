
namespace Kafka.Operations.DeadLetter;

public interface IProducingData<TKey, TValue>:
  IKafkaDeadLetterProp<TKey, TValue>,
  IProduceErrorProp;