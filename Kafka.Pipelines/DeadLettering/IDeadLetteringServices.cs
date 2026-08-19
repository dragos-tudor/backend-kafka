namespace Kafka.Pipelines;

public interface IDeadLetteringServices<TKey, TPayload> :
  IReadDeadLetteringInboxMessagesService<TKey, TPayload>,
  Operations.DeadLetter.IConvertingServices,
  Operations.DeadLetter.IInsertingServices<TKey, TPayload>,
  IDeadLetteringBatchSizeService
{
}
