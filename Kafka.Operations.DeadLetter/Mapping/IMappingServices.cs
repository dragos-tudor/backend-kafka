
namespace Kafka.Operations.DeadLetter;

 public interface IMappingServices<TKey, TValue, TPayload> :
  IKafkaValueMapper<TPayload, TValue>,
  IInstrumentationServices;