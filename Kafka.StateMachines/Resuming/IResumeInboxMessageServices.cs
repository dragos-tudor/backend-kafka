
namespace Kafka.StateMachines;

public interface IResumeInboxMessageServices<TKey, TValue, TPayload, TSession> :
  IGetInboxMessagesService<TKey, TPayload>,
  IHandleInboxMessageServices<TKey, TValue, TPayload, TSession>,
  IScheduleInboxMessageServices<TKey, TPayload>,
  Operations.Inbox.IDispatchDeadLetterServices<TKey, TValue, TPayload>,
  Operations.Inbox.IDelayDeadLetterServices<TKey, TValue, TPayload>,
  IResumeBatchSizeService
  where TSession : IDisposable;