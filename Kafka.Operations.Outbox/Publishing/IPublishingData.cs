
namespace Kafka.Operations.Outbox;

public interface IPublishingData<TKey, TValue, TPayload>:
  IOutboxMessageProp<TKey, TPayload>,
  IPipelineProp,
  IPublishErrorProp;