
namespace Kafka.Operations.Outbox;

public interface IScheduleOutboxMessageServices<TKey, TPayload>:
  IScheduleOptionsService,
  IInstrumentationServices,
  IUpdateIntegrationMessageService<TKey, TPayload>,
  IUtcDateService;