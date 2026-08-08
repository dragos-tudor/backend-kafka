
namespace Kafka.Operations.Inbox;

public interface IDispatchDeadLetterServices<TKey, TValue, TPayload> :
  IDeadLetterTopicService<TKey, TPayload>,
  IProducerService<TKey, TValue>,
  IUtcDateService,
  IInstrumentationServices,
  IKafkaValueMapperService<TPayload, TValue>,
  IUpdateIntegrationMessageService<TKey, TPayload>;