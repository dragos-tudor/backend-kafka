
namespace Kafka.Operations.Inbox;

public interface IDelayingServices<TKey, TValue, TPayload> :
  IDelayOptionsService,
  IInstrumentationServices,
  IUpdateIntegrationMessageService<TKey, TPayload>,
  IUtcDateService;
