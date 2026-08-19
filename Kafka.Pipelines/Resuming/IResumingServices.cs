
namespace Kafka.Pipelines;

public interface IResumingServices<TKey, TPayload, TSession> :
  IReadInboxMessagesService<TKey, TPayload>,
  IHandlingServices<TKey, TPayload, TSession>,
  Operations.Inbox.ISchedulingServices<TKey, TPayload>,
  IResumeBatchSizeService
  where TSession : IDisposable;