
namespace Kafka.Operations.Outbox;

public interface IMappingServices<TKey, TValue, TPayload> :
  IKafkaValueMapper<TPayload, TValue>,
  IInstrumentationServices;