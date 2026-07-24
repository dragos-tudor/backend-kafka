
namespace Kafka.Client;

partial class KafkaFuncs
{
  public static Task<string?> HandleProducerMessage<TKey>(
    Message<TKey, byte[]> message,
    PersistenceStatus status,
    ErrorCode error,
    HandlePersistedMessage<TKey> handlePersisted,
    HandleNotPersistedMessage<TKey> handleNotPersisted,
    HandlePossiblyPersistedMessage<TKey> handlePossiblyPersisted) =>
      status switch
      {
        PersistenceStatus.Persisted => handlePersisted(message),
        PersistenceStatus.PossiblyPersisted => handlePossiblyPersisted(message),
        PersistenceStatus.NotPersisted => handleNotPersisted(message, error),
        _ => Task.FromResult(default(string?))
      };
}