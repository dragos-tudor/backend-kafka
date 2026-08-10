
namespace Kafka.Operations.Outbox;

public interface IDelayingServices<TKey, TValue, TPayload> :
  IDelayOptionsService,
  IInstrumentationServices,
  IUpdateIntegrationMessageService<TKey, TPayload>,
  IUtcDateService;
