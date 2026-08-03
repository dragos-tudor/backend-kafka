
namespace Kafka.Engine;

public interface IConsumeKafkaMessageServices<TKey, TValue, TPayload, TSession> :
  IApplyConsumerOffsetServices<TKey, TValue>,
  ICaptureKafkaMessageServices<TKey, TValue>,
  IHandleInboxMessageServices<TKey, TValue, TPayload, TSession>,
  IInsertInboxMessageServices<TKey, TValue, TPayload>,
  IPublishDeadLetterServices<TKey, TValue, TPayload>
  where TSession : IDisposable;

public interface IInstrumentationServices:
  IGetLoggerService,
  IGetActivitySourceService,
  IGetMetricCountersService;

