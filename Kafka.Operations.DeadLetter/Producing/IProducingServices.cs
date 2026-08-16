
namespace Kafka.Operations.DeadLetter;

public interface IProducingServices<TKey, TValue>:
  IInstrumentationServices,
  IProducerDeadLetterService<TKey, TValue>,
  IDeadLetterMessageTopicService,
  IUtcDateService;

public interface IProducerDeadLetterService<TKey, TValue> { IProducer<TKey, TValue?> GetProducer(); }