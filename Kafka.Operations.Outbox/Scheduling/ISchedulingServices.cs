
namespace Kafka.Operations.Outbox;

public interface ISchedulingServices<TKey, TPayload>:
  IRetryMessageOptionsService,
  IInstrumentationServices,
  IUpdateOutboxMessageService<TKey, TPayload>,
  IUtcDateService;