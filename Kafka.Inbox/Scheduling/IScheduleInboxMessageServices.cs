
namespace Kafka.Inbox;

public interface IScheduleInboxMessageServices<TKey, TPayload>:
  IScheduleOptionsService,
  IInstrumentationServices,
  IUpdateIntegrationMessageService<TKey, TPayload>,
  IUtcDateService;