
namespace Kafka.StateMachines;

public interface IResumingServices<TKey, TValue, TPayload, TSession> :
  IGetInboxMessagesService<TKey, TPayload>,
  IHandlingServices<TKey, TValue, TPayload, TSession>,
  Operations.Inbox.ISchedulingServices<TKey, TPayload>,
  Operations.Inbox.IDispatchingServices<TKey, TValue, TPayload>,
  Operations.Inbox.IDelayingServices<TKey, TValue, TPayload>,
  IResumeBatchSizeService
  where TSession : IDisposable;