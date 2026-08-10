
namespace Kafka.Operations.Inbox;

public interface IOffsettingData<TKey, TPayload>:
  IInboxMessageProp<TKey, TPayload>,
  IPipelineProp,
  ITopicPartitionOffsetAppliedProp,
  ITopicPartitionOffsetProp;