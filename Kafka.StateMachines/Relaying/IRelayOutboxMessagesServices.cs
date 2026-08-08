
namespace Kafka.StateMachines;

public interface IRelayOutboxMessagesServices<TKey, TValue, TPayload> :
  IGetOutboxMessagesService<TKey, TPayload>,
  IPublishOutboxMessageServices<TKey, TValue, TPayload>,
  IScheduleOutboxMessageServices<TKey, TPayload>,
  Operations.Outbox.IDispatchDeadLetterServices<TKey, TValue, TPayload>,
  Operations.Outbox.IDelayDeadLetterServices<TKey, TValue, TPayload>,
  IRelayBatchSizeService;