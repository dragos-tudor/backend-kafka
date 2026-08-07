
namespace Kafka.Inbox;

 public interface IInsertInboxMessageServices<TKey, TValue, TPayload> :
  IIntegrationPayloadMapperService<TValue, TPayload>,
  IInsertInboxMessageService<TKey, TPayload>,
  IInstrumentationServices,
  IUtcDateService;