
namespace Kafka.Operations.Inbox;

public interface ISchedulingServices<TKey, TPayload>:
  IScheduleOptionsService,
  IInstrumentationServices,
  IUpdateIntegrationMessageService<TKey, TPayload>,
  IUtcDateService;