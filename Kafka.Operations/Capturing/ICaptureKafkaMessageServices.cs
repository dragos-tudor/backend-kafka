
namespace Kafka.Operations;

public interface ICaptureKafkaMessageServices<TKey, TValue> :
  IInstrumentationServices,
  IConsumerService<TKey, TValue>;