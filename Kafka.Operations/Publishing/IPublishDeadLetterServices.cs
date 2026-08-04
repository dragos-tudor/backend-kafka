
namespace Kafka.Operations;

public interface IPublishDeadLetterServices<TKey, TValue, TPayload> :
  IDeadLetterTopicService<TKey, TPayload>,
  IProducerService<TKey, TValue>,
  IUtcDateService,
  IInstrumentationServices,
  IKafkaMessageMapperService<TPayload, TValue>,
  IUpdateInboxMessageStatusService<TKey, TPayload>;