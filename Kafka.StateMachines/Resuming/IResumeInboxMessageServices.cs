
namespace Kafka.StateMachines;

public interface IResumeInboxMessageServices<TKey, TValue, TPayload, TSession> :
  IGetInboxMessagesService<TKey, TPayload>,
  IHandleInboxMessageServices<TKey, TValue, TPayload, TSession>,
  IScheduleInboxMessageServices<TKey, TPayload>,
  IDispatchDeadLetterServices<TKey, TValue, TPayload>,
  IDelayDeadLetterServices<TKey, TValue, TPayload>,
  IResumeBatchSizeService
  where TSession : IDisposable;