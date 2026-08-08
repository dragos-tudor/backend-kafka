
namespace Kafka.StateMachines;

public interface IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession> :
  ICaptureKafkaMessageServices<TKey, TValue>,
  IHandleInboxMessageServices<TKey, TValue, TPayload, TSession>,
  IInsertInboxMessageServices<TKey, TValue, TPayload>,
  IOffsetConsumerServices<TKey, TValue>,
  Operations.Inbox.IDispatchDeadLetterServices<TKey, TValue, TPayload>
  where TSession : IDisposable;

