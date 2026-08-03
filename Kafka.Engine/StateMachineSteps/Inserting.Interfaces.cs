
namespace Kafka.Engine;

 public interface IInsertInboxMessageServices<TKey, TValue, TPayload> :
  IGetUtcDateService,
  IInstrumentationServices,
  IInsertInboxMessageService<TKey, TPayload>,
  IPersistedMessageMapperService<TValue, TPayload>;

 public interface IInsertInboxMessageData<TKey, TValue, TPayload>:
  IInboxMessageData<TKey, TPayload>,
  IKafkaMessageData<TKey, TValue>,
  ITopicPartitionOffsetData;