
namespace Kafka.Operations.Outbox;

public interface IDelayDeadLetterServices<TKey, TValue, TPayload> :
  IDelayOptionsService,
  IInstrumentationServices,
  IUpdateIntegrationMessageService<TKey, TPayload>,
  IUtcDateService;
