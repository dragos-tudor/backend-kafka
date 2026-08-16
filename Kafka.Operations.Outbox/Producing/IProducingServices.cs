
namespace Kafka.Operations.Outbox;

public interface IProducingServices<TKey, TValue, TPayload>:
  IInstrumentationServices,
  IKafkaOptionsService,
  IProducerService<TKey, TValue>,
  IOutboxMessageTopicService<TKey, TPayload>,
  IUpdateOutboxMessageService<TKey, TPayload>,
  IUtcDateService;
