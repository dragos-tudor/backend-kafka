
namespace Kafka.Operations.Outbox;

public interface IPublishingServices<TKey, TValue, TPayload>:
  IInstrumentationServices,
  IKafkaValueMapperService<TPayload, TValue>,
  IProducerService<TKey, TValue>,
  IOutboxTopicService<TKey, TPayload>,
  IUpdateIntegrationMessageService<TKey, TPayload>,
  IUtcDateService;
