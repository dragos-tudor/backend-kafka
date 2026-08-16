
namespace Kafka.Messages;

public interface IKafkaValueMapper<TPayload, TValue> { TValue ToKafkaValue(TPayload value); }