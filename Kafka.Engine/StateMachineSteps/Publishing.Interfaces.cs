
namespace Kafka.Engine;

public interface IPublishDeadLetterServices<TKey, TValue, TPayload> :
  IGetDeadLetterTopicService<TKey, TPayload>,
  IGetProducerService<TKey, TValue>,
  IGetUtcDateService,
  IInstrumentationServices,
  IKafkaMessageMapperService<TPayload, TValue>,
  IUpdateInboxMessageStatusService<TKey, TPayload>;

public interface IPublishDeadLetterData<TKey, TValue, TPayload>:
  IKafkaMessageData<TKey, TValue>,
  IInboxMessageData<TKey, TPayload>,
  ITopicPartitionOffsetData,
  IDomainErrorData;