
namespace Kafka.Operations.Outbox;

public interface ISchedulingServices<TKey, TPayload>:
  IScheduleOptionsService,
  IInstrumentationServices,
  IUpdateIntegrationMessageService<TKey, TPayload>,
  IUtcDateService;