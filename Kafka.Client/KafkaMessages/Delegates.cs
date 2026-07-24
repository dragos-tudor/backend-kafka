
namespace Kafka.Client;

public delegate Task<string?> HandlePersistedMessage<TKey>(Message<TKey, byte[]> message);

public delegate Task<string?> HandleNotPersistedMessage<TKey>(Message<TKey, byte[]> message, ErrorCode error);

public delegate Task<string?> HandlePossiblyPersistedMessage<TKey>(Message<TKey, byte[]> message);

public delegate Task<string?> HandleConsumerMessage<TKey>(Message<TKey, byte[]> messsage);