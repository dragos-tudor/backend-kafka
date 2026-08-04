
namespace Kafka.Operations;

public interface IPublishDeadLetterServices<TKey, TValue, TPayload> :
  IKafkaDeadLetterTopicService<TKey, TPayload>,
  IProducerService<TKey, TValue>,
  IUtcDateService,
  IInstrumentationServices,
  IKafkaMessageMapperService<TPayload, TValue>,
  IUpdateInboxMessageStatusService<TKey, TPayload>;