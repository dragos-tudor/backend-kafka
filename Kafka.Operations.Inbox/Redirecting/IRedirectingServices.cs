
namespace Kafka.Operations.Inbox;

public interface IRedirectingServices<TKey, TValue, TPayload>:
  IInstrumentationServices,
  IKafkaOptionsService,
  IProducerService<TKey, TValue>,
  IKafkaDeadLetterTopicService<TKey, TValue>,
  IUtcDateService;
