
namespace Kafka.Operations.Inbox;

public interface ISchedulingServices<TKey, TPayload>:
  IRetryMessageOptionsService,
  IInstrumentationServices,
  IUpdateInboxMessageService<TKey, TPayload>,
  IUtcDateService;