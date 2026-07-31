
namespace Kafka.Engine;

public interface IKafkaMessageValueService<TPayload, TValue> { TValue ToKafkaMessageValue(TPayload value); }

public interface IPersistedMessagePayloadService<TValue, TPayload> { TPayload ToPersistedMessagePayload(TValue value); }
