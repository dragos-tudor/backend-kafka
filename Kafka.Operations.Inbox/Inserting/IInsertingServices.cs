
namespace Kafka.Operations.Inbox;

 public interface IInsertingServices<TKey, TValue, TPayload> :
  IIntegrationPayloadMapperService<TValue, TPayload>,
  IInsertInboxMessageService<TKey, TPayload>,
  IInstrumentationServices,
  IUtcDateService;