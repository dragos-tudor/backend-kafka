
namespace Kafka.Operations.Outbox;

public interface IValidatingServices<TKey, TPayload> :
  IOutboxPayloadValidator<TPayload>,
  IInstrumentationServices;