
namespace Kafka.Inbox;

public interface IDelayDeadLetterServices<TKey, TValue, TPayload> :
  IDelayOptionsService,
  IInstrumentationServices,
  IUpdateIntegrationMessageService<TKey, TPayload>,
  IUtcDateService;
